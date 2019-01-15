import { Injectable, Injector } from '@angular/core'
import { Server } from '../model/server'
import { ApiService } from './api-service'

@Injectable()
export class ServersService extends ApiService<Server> {
    
    constructor(injector: Injector) {
        super(Server, 'servers', injector);
    }
}