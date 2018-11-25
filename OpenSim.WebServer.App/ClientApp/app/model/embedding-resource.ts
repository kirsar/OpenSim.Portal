import { Observable, of } from 'rxjs';
import { Resource, ResourceHelper } from 'hal-4-angular';
import { ValueAndStream } from './value-and-stream';

export abstract  class EmbeddingResource extends Resource {
    public constructor() {
        super();
        this._links = new Object();
    }

    public id?: number;
    protected _embedded: any = new Object();

    private queriedRelations: { [relation: string]: any; } = {};

    private get isLocal(): boolean { return this.id == undefined; }

    protected getOrQueryResource<T extends Resource>(
        type: { new(): T; },
        relation: string,
        getter: (embedded: any) => T,
        setter: (value: T) => void)
        : ValueAndStream<T | undefined> 
    {
        const value = getter(this._embedded);

        if (this.isLocal || value != undefined)
            return new ValueAndStream(value, of(value));

        if (relation in this.queriedRelations)
            return new ValueAndStream(undefined, this.queriedRelations[relation] as Observable<T | undefined>);

        const stream = this.getRelation(type, relation);
        stream.subscribe(res => setter(res));
        this.queriedRelations[relation] = stream;
        return new ValueAndStream<T | undefined>(undefined, stream);
    }

    protected getOrQueryResourceArray<T extends Resource>(
        type: { new(): T; },
        relation: string,
        getter: (embedded: any) => T[],
        setter: (value: T[]) => void)
        : ValueAndStream<T[]>
    {
        const value = getter(this._embedded);

        if (this.isLocal || value != undefined)
            return new ValueAndStream(value, of(value));

        if (relation in this.queriedRelations)
            return new ValueAndStream([], this.queriedRelations[relation] as Observable<T[]>);

        const stream = this.getRelationArray(type, relation);
        stream.subscribe(res => setter(res));
        this.queriedRelations[relation] = stream;
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