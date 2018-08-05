import { EmbeddingResource } from './embedding-resource';
import { User } from './user';
import { Simulation } from './simulation'

export class Presentation extends EmbeddingResource {
    public name?: string;
    public description?: string;

    public get author(): User { return this._embedded.author; }
    public get simulations(): Simulation[] { return this._embedded.simulations; }
}