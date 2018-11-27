import { Observable } from 'rxjs';
import { EmbeddingResource } from './embedding-resource';
import { ValueAndStream } from './value-and-stream';
import { User } from './user';
import { Presentation } from './presentation'
import { SimulationRequestBuilder } from './../service/request-builder/simulation.builder'
import { PresentationRequestBuilder } from './../service/request-builder/presentation.builder'

export class Simulation extends EmbeddingResource {
    public name?: string;
    public description?: string;

    public get author(): User | undefined {
        return this.getOrQueryResource(User, 'author',
            () => this._embedded.author, (value: User)=> this._embedded.author = value).value;
    }

    private getOrQueryReferences(builder?: SimulationRequestBuilder): ValueAndStream<Simulation[]> {
        return this.getOrQueryResourceArray(Simulation, 'references',
            () => this._embedded.references, value => this._embedded.references = value, builder);
    }

    public get references(): Simulation[] { return this.getOrQueryReferences().value; }
    public queryReferences(builder?: SimulationRequestBuilder): Observable<Simulation[]> {
         return this.getOrQueryReferences(builder).stream;
    }
    
    public get consumers(): Simulation[] {
        return this.getOrQueryResourceArray(Simulation, 'consumers',
            () => this._embedded.consumers, value => this._embedded.consumers = value).value;
    }

    public getOrQueryPresentations(builder?: PresentationRequestBuilder): ValueAndStream<Presentation []> {
        return this.getOrQueryResourceArray(Presentation, 'presentations',
            () => this._embedded.presentations, value => this._embedded.presentations = value, builder);
    }

    public get presentations(): Presentation[] { return this.getOrQueryPresentations().value; }
    public queryPresentations(builder?: PresentationRequestBuilder): Observable<Presentation[]> {
         return this.getOrQueryPresentations(builder).stream;
    }
}