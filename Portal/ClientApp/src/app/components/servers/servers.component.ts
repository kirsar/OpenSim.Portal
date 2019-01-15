import { Component, } from '@angular/core';
import { Server } from '../../model/server';
import { StorageService } from '../../service/storage-service'
import { ServerRequestBuilder } from '../../service/request-builder/server.builder'

@Component({
    selector: 'servers',
    templateUrl: './servers.component.html',
    styleUrls: ['./servers.component.css']
})
export class ServersComponent {
    public servers: Server[] = [];
   
    constructor(private readonly service: StorageService) {
        this.queryServers();
    }

    private queryServers = () => 
        this.service.getServers(new ServerRequestBuilder()
            .withSimulations()
            .withPresentations()
            .withAuthor()).subscribe((servers: Server[]) => this.servers = servers);

    public onServerCreated(server: Server) {
        this.servers.push(server);
    }
}
