import { Resource } from 'hal-4-angular'
import { User } from './user';
import { Presentation } from "./presentation"

export class Simulation extends Resource {
    id?: number;
    name?: string;
    description?: string;

    _embedded?: any;

    get author(): User { return this._embedded.author; }
    get references(): Simulation[] { return this._embedded.references; }
    get consumers(): Simulation[] { return this._embedded.consumers; }
    get presentations(): Presentation[] { return this._embedded.presentations; }
}