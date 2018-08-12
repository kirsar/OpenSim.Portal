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

        // TODO hot fix for request with relative uri sent from Resource
        const link = this._links[relation];
        const rootUri = ResourceHelper.getRootUri();
        if (!link.href.startsWith(rootUri))
            link.href = ResourceHelper.getRootUri() + link.href.substr(1);
        
        return super.getRelationArray(type, relation);
    }
}