import { Observable, of } from 'rxjs';
import { Resource, ResourceHelper } from 'hal-4-angular';

export abstract  class EmbeddingResource extends Resource {
    public constructor() {
        super();
        this._links = new Object();
    }

    public id?: number;
    protected _embedded: any = new Object();

    protected getSelfQueryResource<T extends Resource>(
            type: { new(): T; },
            relation: string,
            getter: (embedded: any) => T,
            setter: (value: T) => void):
        T | undefined {
        if (this.id == undefined) // TODO is it ok for locally created objects?
            return undefined;

        const value = getter(this._embedded);
        if (value != undefined)
            return value;

        this.getRelation(type, relation).subscribe(res => setter(res));
        return undefined;
    }

    protected getSelfQueryResourceArray<T extends Resource>(
        type: { new(): T; },
        relation: string,
        getter: (embedded: any) => T[],
        setter: (value: T[]) => void): T[] {
        if (this.id == undefined) // TODO is it ok for locally created objects?
            return [];

        const value = getter(this._embedded);
        if (value != undefined)
            return value;

        this.getRelationArray(type, relation).subscribe(res => setter(res));
        return [];
    }

    public getRelation<TRelation extends Resource>(type: { new(): TRelation; }, relation: string): Observable<TRelation> {
        if (!(relation in this._links))
            return of();

        // TODO hot fix for request with relative uri sent from Resource
        const link = this._links[relation];
        const rootUri = ResourceHelper.getRootUri();
        if (!link.href.startsWith(rootUri))
            link.href = ResourceHelper.getRootUri() + link.href.substr(1);

        return super.getRelation(type, relation);
    }

    public getRelationArray<TRelation extends Resource>(type: { new(): TRelation; }, relation: string): Observable<TRelation[]> {
        if (!(relation in this._links))
            return of([]);

        // TODO hot fix for request with relative uri sent from Resource
        const link = this._links[relation];
        const rootUri = ResourceHelper.getRootUri();
        if (!link.href.startsWith(rootUri))
            link.href = ResourceHelper.getRootUri() + link.href.substr(1);

        return super.getRelationArray(type, relation);
    }
}