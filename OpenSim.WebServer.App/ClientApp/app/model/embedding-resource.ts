import { Observable, of } from 'rxjs';
import { Resource, ResourceHelper } from 'hal-4-angular';

export abstract  class EmbeddingResource extends Resource {
    public constructor() {
        super();
        this._links = new Object();
    }

    public id?: number;
    protected _embedded: any = new Object();

    public getRelationArray<TRelation extends Resource>(type: { new(): TRelation; }, relation: string): Observable<TRelation[]> {
        if (!(relation in this._links))
            return of([]);

        let relations = this._links[relation];
        relations = Array.isArray(relations) ? relations : [relations];

        // TODO hot fix for request with relative uri sent from Resource
        //relations.forEach((r: any) => r.href = ResourceHelper.getRootUri() + r.href);
        relations.forEach((r: any) => r.href = 'http://localhost:60772/api/v1/simulations/1');
        
        return super.getRelationArray(type, relation);
    }
}