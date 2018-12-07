import { Injectable } from '@angular/core'
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators'

import { Server } from './../model/server'
import { Simulation } from './../model/simulation'
import { Presentation } from './../model/presentation'

import { ServersService } from './servers.service'
import { SimulationsService } from './simulations.service'
import { PresentationsService } from './presentations.service'
import { RequestBuilder } from './request-builder/request-builder-interface'

// just a little peace of storage is implemented to see how it works with lazy related resource initialization
@Injectable()
export class StorageService {

    constructor(
        private readonly serverService: ServersService,
        private readonly simulationService: SimulationsService,
        private readonly presentationService: PresentationsService) {
    }

    private servers!: Map<number, Server>;
    private serversArr : Server[] | undefined;

    public getServers(builder?: RequestBuilder<Server>): Observable<Server[]> {
        if (this.servers != undefined)
            return of(Array.from(this.servers.values()));

        return this.serverService.getAll(builder).pipe(map(servers => {
            this.servers = new Map(servers.map(server => [server.id, server] as [number, Server]));
            return servers;
        }));
    }

    public getServer(id: number, builder?: RequestBuilder<Server>): Observable<Server | undefined> {
        const server = this.servers.get(id);
        if (server != undefined)
            return of(server);

        const stream = this.serverService.get(id, builder);
        stream.subscribe(s => {
            if (s != undefined)
                this.servers.set(id, s);
        });
        return stream;
    }
}


