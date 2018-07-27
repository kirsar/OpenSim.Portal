import { Injectable, Injector } from '@angular/core'
import { Observable } from 'rxjs';
import { RestService, HalOptions } from 'hal-4-angular'
import { Server } from '../model/server'

@Injectable()
export class ServerService {
    private readonly service: RestService<Server>;

    constructor(injector: Injector) {
        this.service = new RestService<Server>(Server, 'servers', injector);
    }

    public getAll(): Observable<Server[]> {
        const options: HalOptions =
        {
            params: [
                {
                    key: "fields",
                    value: "_embedded/servers(" +
                        "id,name,description,isRunning," +
                        "_links/self," +
                        "_embedded(" +
                        "author(id,name,_links/self)," +
                        "simulations(id,name,description,_links/self)," +
                        "presentations(id,name,description,_links/self))"
                }
            ]
        };

        return this.service.getAll(options);
    }
}