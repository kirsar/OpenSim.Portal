import { Component, Inject, NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { Http } from "@angular/http";

@Component({
    selector: "new-server",
    templateUrl: "./new-server.component.html",
    styleUrls: ["./new-server.component.css"]
})
export class NewServerComponent {
    public simulations: Simulation[];

    constructor(http: Http, @Inject("BASE_URL") baseUrl: string) {
        http.get(baseUrl + "api/v1/simulations").subscribe(
            results => this.simulations = results.json()._embedded.simulations as Simulation[],
            error => console.error(error));
    }
}

//interface Server {
//    name: string;
//    description: string;
//    isRunning: boolean;
//    _embedded: Embedded;
//}

//interface Embedded {
//    author: Author;
//    simulations: Simulation[];
//    presentations: Presentation[];
//}

//interface Author {
//    name: string;
//}

interface Simulation {
    name: string;
    description: string;
}

//interface Presentation {
//    name: string;
//    description: string;
//}
