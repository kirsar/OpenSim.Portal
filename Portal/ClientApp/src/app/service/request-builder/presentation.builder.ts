import { Injectable } from '@angular/core'
import { RequestBuilder } from './request-builder-interface'
import { Presentation } from '../../model/presentation'
import { UserRequestBuilder } from './user.builder'
import { SimulationRequestBuilder } from './simulation.builder'

@Injectable()
export class PresentationRequestBuilder extends RequestBuilder<Presentation> {
    constructor() {
        super(['name', 'description']);
    }

    public withAuthor(builder?: UserRequestBuilder): PresentationRequestBuilder {
        this.addRelation(UserRequestBuilder, 'author', builder);
        return this;
    }

    public withSimulations(builder?: SimulationRequestBuilder): PresentationRequestBuilder {
        this.addRelation(SimulationRequestBuilder, 'simulations', builder);
        return this;
    }
}