import { Injectable, Injector } from '@angular/core'
import { Observable } from 'rxjs';
import { RestService } from 'hal-4-angular'
import { Server } from '../model/server'

@Injectable()
export class ServerService extends RestService<Server> {
    constructor(injector: Injector) {
        super(Server, 'servers', injector);
    }

    public getAll(): Observable<Server[]> {
        return super.getAll();
    }
}