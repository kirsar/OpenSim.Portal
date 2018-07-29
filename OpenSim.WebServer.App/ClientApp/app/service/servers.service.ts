import { Component, Inject, Output, EventEmitter } from "@angular/core";

import { Injectable, Injector } from '@angular/core'
import { Observable } from 'rxjs';
import { RestService, HalOptions } from 'hal-4-angular'
import { Server } from '../model/server'
import { ServerRequestBuilder } from "../service/request-builder/server.builder"
import { map } from "rxjs/operators"

@Injectable()
export class ServersService {
    private readonly service: RestService<Server>;
    private readonly resource: string = 'servers';

    constructor(injector: Injector) {
        this.service = new RestService<Server>(Server, this.resource, injector);
    }

    public getAll(builder?: ServerRequestBuilder): Observable<Server[]> {
        const options: HalOptions = builder != undefined ? {
                params: [
                    {
                        key: "fields",
                        value: `_embedded/${this.resource}(${builder.build()})`
                    }
                ]
            } : {};

        return this.service.getAll(options);
    }


    // TODO make PR in fork hal-4-angular to support options in get(id)
    public get(id: number, builder?: ServerRequestBuilder): Observable<Server | undefined> {
        const options: HalOptions = builder != undefined ? {
            params: [
                {
                    key: "fields",
                    value: `_embedded/${this.resource}(${builder.build()})`
                }
            ]
        } : {};

        return this.service.getAll(options).pipe(map(servers => {
             debugger;

            for (let server of servers) {
                if (server.id == id)
                    return server;
            }

            return undefined;
        }));
    }
}