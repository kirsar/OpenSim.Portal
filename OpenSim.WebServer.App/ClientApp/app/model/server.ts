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

    public addSimulation(id: number | undefined) {
        this._links.simulations.push(new Link('simulations', id));
    }

    public addPresentation(id: number | undefined) {
        this._links.presentations.push(new Link('presentations', id));
    }
}