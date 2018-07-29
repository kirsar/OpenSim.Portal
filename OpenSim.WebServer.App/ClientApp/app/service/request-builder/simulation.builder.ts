import { Injectable } from '@angular/core'
import { Simulation } from "../../model/simulation"
import { RequestBuilder } from "./request-builder-interface"
import { UserRequestBuilder } from "./user.builder"
import { PresentationRequestBuilder } from "./presentation.builder"

@Injectable()
export class SimulationRequestBuilder extends RequestBuilder<Simulation> {
    constructor() {
        super([
            "id",
            "name",
            "description",
            "_links/self"]);
    }
   
    withAuthor(builder?: UserRequestBuilder): SimulationRequestBuilder {
        this.addParam(`author(${(builder ? builder : new UserRequestBuilder()).build()})`);
        return this;
    }

    withReferences(builder?: SimulationRequestBuilder): SimulationRequestBuilder {
        this.addParam(`references(${(builder ? builder : new SimulationRequestBuilder()).build()})`);
        return this;
    }

    withConsumers(builder?: SimulationRequestBuilder): SimulationRequestBuilder {
        this.addParam(`consumers(${(builder ? builder : new SimulationRequestBuilder()).build()})`);
        return this;
    }

    withPresentations(builder?: PresentationRequestBuilder): SimulationRequestBuilder {
        this.addParam(`presentations(${(builder ? builder : new PresentationRequestBuilder()).build()})`);
        return this;
    }
}