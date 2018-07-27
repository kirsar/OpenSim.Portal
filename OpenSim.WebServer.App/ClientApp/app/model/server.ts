import { Resource } from 'hal-4-angular';
import { User } from './user';
import { Simulation } from './simulation';
import { Presentation } from './presentation';

export class Server extends Resource {
    id?: number;
    name?: string;
    description?: string;

    // TODO think aobut how to deal woth _embedded
    //author?: User;
    //simulations: Simulation[] = [];
    //presentations: Presentation[] = [];
}