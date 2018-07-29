import { EmbeddingResource } from './embedding-resource';
import { User } from './user';
import { Simulation } from './simulation';
import { Presentation } from './presentation';

export class Server extends EmbeddingResource {
    public name?: string;
    public description?: string;

    public get author(): User { return this._embedded.author; }
    public get simulations(): Simulation[] { return this._embedded.simulations; }
    public get presentations(): Presentation[] { return this._embedded.presentations; }
}