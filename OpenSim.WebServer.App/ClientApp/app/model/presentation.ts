import { EmbeddingResource } from './embedding-resource';
import { User } from './user';
import { Simulation } from './simulation'

export class Presentation extends EmbeddingResource {
    public name?: string;
    public description?: string;

    public get author(): User | undefined {
        return this.getSelfQueryResource(User, 'author',
            () => this._embedded.author, (value: User) => this._embedded.author = value);
    }
    
    public get simulations(): Simulation[] {
        return this.getSelfQueryResourceArray(Simulation, 'simulations',
            () => this._embedded.simulations, value => this._embedded.simulations = value);
    }
}