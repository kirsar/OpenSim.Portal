import { Injectable } from '@angular/core'
import { Observable, of } from 'rxjs';

import { Server } from './../model/server'
import { Simulation } from './../model/simulation'
import { Presentation } from './../model/presentation'

import { ServersService } from './servers.service'
import { SimulationsService } from './simulations.service'
import { PresentationsService } from './presentations.service'
import { RequestBuilder } from './request-builder/request-builder-interface'

@Injectable()
export class StorageService {

    constructor(
        private readonly serverService: ServersService,
        private readonly simulationService: SimulationsService,
        private readonly presentationService: PresentationsService) {
    }

    private servers!: Map<number, Server>;

    public getServers(builder?: RequestBuilder<Server>): Observable<Server[]> {
        if (this.servers != undefined)
            return of(Array.from(this.servers.values()));

        const stream = this.serverService.getAll(builder);
        stream.subscribe(servers => this.servers = new Map(servers.map(server => [server.id, server] as [number, Server])));
        return stream;
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


