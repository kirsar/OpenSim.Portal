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
        return this.getOrQueryResource(User, 'author',
            () => this._embedded.author, (value: User) => this._embedded.author = value).value;
    }
       
    public get simulations(): Simulation[] {
        return this.getOrQueryResourceArray(Simulation, 'simulations',
            () => this._embedded.simulations, (value: Simulation[]) => this._embedded.simulations = value).value;
    }

    public get presentations(): Presentation[] {
        return this.getOrQueryResourceArray(Presentation, 'presentations',
            () => this._embedded.presentations, (value: Presentation[]) => this._embedded.presentations = value).value;
    }

    public addSimulation(simulation: Simulation) {
        this._links.simulations.push(simulation._links.self);

        //if (this._embedded.simulations == undefined)
        //    this._embedded.simulations = [];

        //this._embedded.simulations.push(simulation);
    }

    public addPresentation(presentation: Presentation) {
        this._links.presentations.push(presentation._links.self);
    }
}