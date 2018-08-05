import { Resource } from 'hal-4-angular';

export abstract  class EmbeddingResource extends Resource {
    public constructor() {
        super();
        this._links = new Object();
    }

    public id?: number;
    protected _embedded: any = new Object();
}