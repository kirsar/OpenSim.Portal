import { Component, } from '@angular/core';
import { Server } from '../../model/server';
import { ServersService } from '../../service/servers.service'
import { ServerRequestBuilder } from '../../service/request-builder/server.builder'

@Component({
    selector: 'servers',
    templateUrl: './servers.component.html',
    styleUrls: ['./servers.component.css']
})
export class ServersComponent {
    public servers: Server[] = [];
   
    constructor(private readonly service: ServersService) {
        this.queryServers();
    }

    private queryServers = () => 
        this.service.getAll(new ServerRequestBuilder()
            .withSimulations()
            .withPresentations()
            .withAuthor()).subscribe(
                (servers: Server[]) => this.servers = servers,
                (error: any) => console.error(error));

    public onServerCreated(server: Server) {
        //this.servers.push(server); // TODO just add result to list when servers in both components are compatible
        this.queryServers();
    }
}
