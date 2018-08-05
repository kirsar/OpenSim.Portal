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
        this.addParam(`author(${(builder ? builder : new UserRequestBuilder()).build()})`);
        return this;
    }

    public withSimulations(builder?: SimulationRequestBuilder): ServerRequestBuilder {
        this.addParam(`simulations(${(builder ? builder : new SimulationRequestBuilder()).build()})`);
        return this;
    }

    public withPresentations(builder?: PresentationRequestBuilder): ServerRequestBuilder {
        this.addParam(`presentations(${(builder ? builder : new PresentationRequestBuilder()).build()})`);
        return this;
    }
}