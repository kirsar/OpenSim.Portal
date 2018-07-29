import { Resource } from 'hal-4-angular';

export abstract  class EmbeddingResource extends Resource {
    public id?: number;
    protected _embedded?: any;
}