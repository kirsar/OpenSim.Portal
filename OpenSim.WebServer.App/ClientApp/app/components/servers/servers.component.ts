import { Component, Inject, NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { Http } from "@angular/http";

@Component({
    selector: "servers",
    templateUrl: "./servers.component.html",
    styleUrls: ["./servers.component.css"]
})
export class ServersComponent {
    public servers: Server[];

    constructor(http: Http, @Inject("BASE_URL") baseUrl: string) {
        http.get(baseUrl +
            "api/v1/servers?fields=_embedded/servers(" +
                "id,name,description,isRunning,_embedded(" +
                    "author(name,_links/self)," +
                    "simulations(name,description,_links/self)," +
                    "presentations(name,description,_links/self)))").subscribe(result => {
                debugger;
                const simulations = result.json()._embedded.servers[0]._embedded.simulations as Simulation[];
                const presentations = result.json()._embedded.servers[0]._embedded.presentations as Presentation[];
                const author = result.json()._embedded.servers[0]._embedded.author as Author;
                const server = result.json()._embedded.servers[0] as Server;
                const serverss = result.json()._embedded.servers as Server[];

                this.servers = serverss;
            },
            error => console.error(error));
    }
}

interface Server {
    id: number;
    name: string;
    description: string;
    isRunning: boolean;
    _embedded: Embedded;
}

interface Embedded {
    author: Author;
    simulations: Simulation[];
    presentations: Presentation[];
}

interface Author {
    name: string;
}

interface Simulation {
    name: string;
    description: string;
}

interface Presentation {
    name: string;
    description: string;
}
