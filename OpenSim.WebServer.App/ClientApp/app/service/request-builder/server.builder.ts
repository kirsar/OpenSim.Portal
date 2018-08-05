import { Injectable } from '@angular/core'
import { Server } from '../../model/server'
import { RequestBuilder } from './request-builder-interface'
import { UserRequestBuilder } from './user.builder'
import { SimulationRequestBuilder } from './simulation.builder'
import { PresentationRequestBuilder } from './presentation.builder'

@Injectable()
export class ServerRequestBuilder extends RequestBuilder<Server> {
    constructor() {
        super(['name', 'description']);
    }

    public withAuthor(builder?: UserRequestBuilder): ServerRequestBuilder {
        this.addRelation(UserRequestBuilder, 'author', builder);
        return this;
    }

    public withSimulations(builder?: SimulationRequestBuilder): ServerRequestBuilder {
        this.addRelation(SimulationRequestBuilder, 'simulations', builder);
        return this;
    }

    public withPresentations(builder?: PresentationRequestBuilder): ServerRequestBuilder {
        this.addRelation(PresentationRequestBuilder, 'presentations', builder);
        return this;
    }
}