import { Component, Inject, NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { Http } from "@angular/http";

@Component({
    selector: 'servers',
    templateUrl: './servers.component.html',
    styleUrls: ['./servers.component.css']
})
export class ServersComponent {
    public servers: Server[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/v1/server?fields=' +
            'name,description,isRunning,' +
                'author(name),' +
                'simulations(name),' +
                'presentations(name)').subscribe(result => {
            this.servers = result.json() as Server[];
        }, error => console.error(error));
    }
}

interface Server {
    id: number;
    name: string;
    description: string;
    isRunning: boolean;
    simulations: Simulation[];
    presentations: Presentation[];
}

interface Author {
    name: string;
}

interface Simulation {
    id: number;
    name: string;
    description: string;
}

interface Presentation {
    id: number;
    name: string;
    description: string;
}
