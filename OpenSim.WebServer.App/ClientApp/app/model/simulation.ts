import { Observable } from 'rxjs';
import { EmbeddingResource } from './embedding-resource';
import { User } from './user';
import { Presentation } from './presentation'

export class Simulation extends EmbeddingResource {
    public name?: string;
    public description?: string;

    public get author(): User { return this._embedded.author; }
    public get references(): Simulation[] { return this._embedded.references; }
    public get consumers(): Simulation[] { return this._embedded.consumers; }
    public get presentations(): Presentation[] { return this._embedded.presentations; }

    public queryReferences(): Observable<Simulation[]> { return this.getRelationArray(Simulation, 'references'); }
    public queryPresentations(): Observable<Presentation[]> { return this.getRelationArray(Presentation, 'presentations'); }
}