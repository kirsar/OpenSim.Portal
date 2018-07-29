import { Injectable } from '@angular/core'
import { RequestBuilder } from "./request-builder-interface"
import { Presentation } from "../../model/presentation"
import { UserRequestBuilder } from "./user.builder"

@Injectable()
export class PresentationRequestBuilder extends RequestBuilder<Presentation> {
    constructor() {
        super([
            "id",
            "name",
            "description",
            "_links/self"]);
    }

    withAuthor(builder?: UserRequestBuilder): PresentationRequestBuilder {
        this.addParam(`author(${(builder ? builder : new UserRequestBuilder()).build()})`);
        return this;
    }
}