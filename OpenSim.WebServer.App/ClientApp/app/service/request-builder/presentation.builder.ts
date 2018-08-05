import { Injectable } from '@angular/core'
import { RequestBuilder } from './request-builder-interface'
import { Presentation } from '../../model/presentation'
import { UserRequestBuilder } from './user.builder'
import { SimulationRequestBuilder } from './simulation.builder'

@Injectable()
export class PresentationRequestBuilder extends RequestBuilder<Presentation> {
    constructor() {
        super([
            'id',
            'name',
            'description',
            '_links/self']);
    }

    public withAuthor(builder?: UserRequestBuilder): PresentationRequestBuilder {
        this.addParam(`author(${(builder ? builder : new UserRequestBuilder()).build()})`);
        return this;
    }

    public withSimulations(builder?: SimulationRequestBuilder): PresentationRequestBuilder {
        this.addParam(`simulations(${(builder ? builder : new SimulationRequestBuilder()).build()})`);
        return this;
    }
}