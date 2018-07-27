import { Resource } from 'hal-4-angular'
import { User } from './user';

export class Presentation extends Resource {
    id?: number;
    name?: string;
    description?: string;
    author?: User;
}