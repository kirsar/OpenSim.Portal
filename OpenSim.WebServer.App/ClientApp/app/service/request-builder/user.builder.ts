import { Injectable } from '@angular/core'
import { User } from '../../model/user'
import { RequestBuilder } from './request-builder-interface'

@Injectable()
export class UserRequestBuilder extends  RequestBuilder<User> {
    constructor() {
        super(['name']);
    }
}