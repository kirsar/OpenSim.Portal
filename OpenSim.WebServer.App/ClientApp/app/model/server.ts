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

    public get author(): User { return this._embedded.author; }
    public get simulations(): Simulation[] { return this._embedded.simulations; }
    public get presentations(): Presentation[] { return this._embedded.presentations; }

    public addSimulation(simulation: Simulation) {
        this._links.simulations.push(simulation._links.self);
    }

    public addPresentation(presentation: Presentation) {
        this._links.presentations.push(presentation._links.self);
    }
}