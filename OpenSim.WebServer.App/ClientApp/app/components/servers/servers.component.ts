import { Component, } from "@angular/core";
import { Server } from "../../model/server";
import { ServerService } from "../../service/server.service"

@Component({
    selector: "servers",
    templateUrl: "./servers.component.html",
    styleUrls: ["./servers.component.css"]
})
export class ServersComponent {
    public servers: Server[] = [];
   
    constructor(private readonly service: ServerService) {
        this.queryServers();
    }

    queryServers = () => 
        this.service.getAll().subscribe(
            (servers: any) => this.servers = servers,
            (error: any) => console.error(error));

    onServerCreated(server: Server) {
        //this.servers.push(server); // TODO just add result to list when servers in both components are compatible
        this.queryServers();
    }
}
