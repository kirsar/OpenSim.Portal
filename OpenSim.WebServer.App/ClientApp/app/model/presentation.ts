import { EmbeddingResource } from './embedding-resource';
import { User } from './user';
import { Simulation } from './simulation'

export class Presentation extends EmbeddingResource {
    public name?: string;
    public description?: string;

    public get author(): User | undefined {
        return this.getOrQueryResource(User, 'author',
            () => this._embedded.author, (value: User) => this._embedded.author = value).value;
    }
    
    public get simulations(): Simulation[] {
        return this.getOrQueryResourceArray(Simulation, 'simulations',
            () => this._embedded.simulations, value => this._embedded.simulations = value).value;
    }
}