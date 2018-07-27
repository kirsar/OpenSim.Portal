import { Component, Inject, } from "@angular/core";
import { HttpClient, } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../service/api-service';

@Component({
    selector: "servers",
    templateUrl: "./server.component.html",
    styleUrls: ["./server.component.css"]
})
export class ServerComponent {
    private sub: any;
    public server?: Server;

    constructor(
        private readonly route: ActivatedRoute,
        private readonly api: ApiService)
    { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params =>
            this.api.get<Server>("servers/" + params['id'] + "?fields=" +
                "name,description,isRunning," +
                "_links/self," +
                "_embedded(" +
                    "author(name,description,_links/self)," +
                    "simulations(name,description,_links/self)," +
                "presentations(name,description,_links/self))").subscribe(
                (result: any) => this.server = result,
                (error: any) => console.error(error)));
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}

interface Server {
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
