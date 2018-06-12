import { Component, Inject, } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
    selector: "new-server-form",
    templateUrl: "./new-server.component.html",
    styleUrls: ["./new-server.component.css"]
})
export class NewServerFormComponent {
    public simulations: Simulation[] = [];

    constructor(private readonly http: HttpClient, @Inject("BASE_URL") private readonly  baseUrl: string) {
        http.get(baseUrl + "api/v1/simulations", { responseType: 'text' }).subscribe(
            res => this.simulations = JSON.parse(res)._embedded.simulations as Simulation[],
            error => console.error(error));
    }

    server = new Server("New Server 1");

    //get isSimulationSelected() {
    //    return this.simulations.filter(s => s.isSelected).length > 0;
    //}

    onCreate() {
        this.simulations.filter(sim => sim.isSelected).forEach(
            sim => this.server.simulations.push(sim));

        const httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/hal+json' })
        };

        debugger;

        const body = JSON.stringify(this.server);
        this.http.post<Server>(this.baseUrl + "api/v1/servers", body, httpOptions);
    }
}

export class Server {
    constructor(public name: string)
    {
        this.simulations = [];
    }

    description?: string;
    simulations: Simulation[];
}

//interface Embedded {
//    author: Author;
//    simulations: Simulation[];
//    presentations: Presentation[];
//}

//interface Author {
//    name: string;
//}

interface Simulation {
    id: number;
    name: string;
    description: string;
    isSelected: boolean;
}

//interface Presentation {
//    name: string;
//    description: string;
//}
