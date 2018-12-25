import { Injectable } from '@angular/core'
import { Simulation } from '../../model/simulation'
import { RequestBuilder } from './request-builder-interface'
import { UserRequestBuilder } from './user.builder'
import { PresentationRequestBuilder } from './presentation.builder'

@Injectable()
export class SimulationRequestBuilder extends RequestBuilder<Simulation> {
    constructor() {
        super(['name','description']);
    }

    public withAuthor(builder?: UserRequestBuilder): SimulationRequestBuilder {
        this.addRelation(UserRequestBuilder, 'author', builder);
        return this;
    }

    public withReferences(builder?: SimulationRequestBuilder): SimulationRequestBuilder {
        this.addRelation(SimulationRequestBuilder, 'references', builder);
        return this;
    }

    public withConsumers(builder?: SimulationRequestBuilder): SimulationRequestBuilder {
        this.addRelation(SimulationRequestBuilder, 'consumers', builder);
        return this;
    }

    public withPresentations(builder?: PresentationRequestBuilder): SimulationRequestBuilder {
        this.addRelation(PresentationRequestBuilder, 'presentations', builder);
        return this;
    }
}