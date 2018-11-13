import { EmbeddingResource } from './embedding-resource';
import { User } from './user';
import { Simulation } from './simulation';
import { Presentation } from './presentation';
import { Link } from './link';

export class Server extends EmbeddingResource {
    public constructor() {
        super();
        this._links.simulations = [];
        this._links.presentations = [];
    }

    public name?: string;
    public description?: string;

    public get author(): User | undefined {
        return this.getSelfQueryResource(User, 'author',
            () => this._embedded.author, (value: User) => this._embedded.author = value);
    }
       
    public get simulations(): Simulation[] {
        return this.getSelfQueryResourceArray(Simulation, 'simulations',
            () => this._embedded.simulations, (value: Simulation[]) => this._embedded.simulations = value);
    }

    public get presentations(): Presentation[] {
        // TODO empty arrays are not supported by WebApi.Hal so no self-query
        return this._embedded.presentations;
    }

    public addSimulation(simulation: Simulation) {
        this._links.simulations.push(simulation._links.self);
    }

    public addPresentation(presentation: Presentation) {
        this._links.presentations.push(presentation._links.self);
    }
}