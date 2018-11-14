import { Observable } from 'rxjs';
import { EmbeddingResource } from './embedding-resource';
import { User } from './user';
import { Presentation } from './presentation'

export class Simulation extends EmbeddingResource {
    public name?: string;
    public description?: string;

    public get author(): User | undefined {
        return this.getSelfQueryResource(User, 'author',
            () => this._embedded.author, (value: User) => this._embedded.author = value);
    }

    public get references(): Simulation[] {
        return this.getSelfQueryResourceArray(Simulation, 'references',
            () => this._embedded.references, value => this._embedded.references = value);
    }

    public get consumers(): Simulation[] {
        return this.getSelfQueryResourceArray(Simulation, 'consumers',
            () => this._embedded.consumers, value => this._embedded.consumers = value);
    }

    public get presentations(): Presentation[] {
        return this.getSelfQueryResourceArray(Presentation, 'presentations',
            () => this._embedded.presentations, value => this._embedded.presentations = value);
    }

    public queryReferences(): Observable<Simulation[]> { return this.getRelationArray(Simulation, 'references'); }
    public queryPresentations(): Observable<Presentation[]> { return this.getRelationArray(Presentation, 'presentations'); }
}