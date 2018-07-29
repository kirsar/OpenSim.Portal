import { Resource } from 'hal-4-angular';
import { User } from './user';
import { Simulation } from './simulation';
import { Presentation } from './presentation';

export class Server extends Resource {
    id?: number;
    name?: string;
    description?: string;

    _embedded?: any;

    get author(): User { return this._embedded.author; }
    get simulations(): Simulation[] { return this._embedded.simulations; }
    get presentations(): Presentation[] { return this._embedded.presentations; }
}