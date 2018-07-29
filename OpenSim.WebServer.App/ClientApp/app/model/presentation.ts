import { EmbeddingResource } from './embedding-resource';
import { User } from './user';

export class Presentation extends EmbeddingResource {
    public name?: string;
    public description?: string;

    public get author(): User { return this._embedded.author; }
}