import { Observable, of } from 'rxjs';
import { Resource, ResourceHelper } from 'hal-4-angular';

export class ValueAndStream<T> {
    public constructor(public value: T, public stream: Observable<T>) { }
}

export abstract  class EmbeddingResource extends Resource {
    public constructor() {
        super();
        this._links = new Object();
    }

    public id?: number;
    protected _embedded: any = new Object();

    // TODO: current back end hateoas implementations doesn't send null and empty list resources
    // we need to know if we requested resource and it's null/empty array or we haven't requested yet
    private queriedRelations = new Set<string>(); 

    protected getOrQueryResource<T extends Resource>(
            type: { new(): T; },
            relation: string,
            getter: (embedded: any) => T,
        setter: (value: T) => void)
        : ValueAndStream<T | undefined> 
    {
        if (this.id == undefined) // TODO is it ok for locally created objects?
            return new ValueAndStream(undefined, of(undefined));

        const value = getter(this._embedded);
        if (value != undefined)
            return new ValueAndStream(value, of(value));

        if (this.queriedRelations.has(relation))
            return new ValueAndStream(undefined, of(undefined));

        this.queriedRelations.add(relation);

        const  stream = this.getRelation(type, relation);
        stream.subscribe(res => setter(res));
        return new ValueAndStream<T | undefined>(undefined, stream);
    }

    protected getOrQueryResourceArray<T extends Resource>(
        type: { new(): T; },
        relation: string,
        getter: (embedded: any) => T[],
        setter: (value: T[]) => void)
        : ValueAndStream<T[]>
    {
        if (this.id == undefined) // TODO is it ok for locally created objects?
            return new ValueAndStream([], of([]));

        const value = getter(this._embedded);
        if (value != undefined)
            return new ValueAndStream(value, of(value));

        if (this.queriedRelations.has(relation))
            return new ValueAndStream([], of([]));

        this.queriedRelations.add(relation);

        const stream = this.getRelationArray(type, relation);
        stream.subscribe(res => setter(res));
        return new ValueAndStream([], stream);
    }

    public getRelation<T extends Resource>(type: { new(): T; }, relation: string): Observable<T> {
        if (!(relation in this._links))
            return of();

        this.fixLinkUri(relation);

        return super.getRelation(type, relation);
    }

    public getRelationArray<T extends Resource>(type: { new(): T; }, relation: string): Observable<T[]> {
        if (!(relation in this._links))
            return of([]);

        this.fixLinkUri(relation);

        return super.getRelationArray(type, relation);
    }

    // TODO hot fix for request with relative uri sent from Resource
    private fixLinkUri(relation: string) {
        const link = this._links[relation];
        const rootUri = ResourceHelper.getRootUri();
        if (!link.href.startsWith(rootUri))
            link.href = ResourceHelper.getRootUri() + link.href.substr(1);
    }
}