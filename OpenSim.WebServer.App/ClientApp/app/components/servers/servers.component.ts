import { Component, Inject, NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { Http } from "@angular/http";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { DropdownModule } from "ngx-dropdown";

@Component({
    selector: 'servers',
    templateUrl: './servers.component.html'
})
export class ServersComponent {
    public servers: Server[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/v1/server').subscribe(result => {
            this.servers = result.json() as Server[];
        }, error => console.error(error));
    }

    printOpening() {
        debugger;
        console.log("opened!");
    }

    printClosing() {
        debugger;
        console.log("closed!");
    }
}

@NgModule({
    imports: [
        BrowserModule,
        DropdownModule
    ],
    declarations: [
        ServersComponent
    ],
    bootstrap: [
        ServersComponent
    ]
})

export class ServerModule {
}

platformBrowserDynamic().bootstrapModule(ServerModule);

interface Server {
    id: number;
    name: string;
    description: string;
    simulations: Simulation[];
    presentations: Presentation[];
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
