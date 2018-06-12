import { Component, Inject, } from "@angular/core";
import { HttpClient, } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

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
        private readonly http: HttpClient, @Inject("BASE_URL")
        private readonly baseUrl: string) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params =>
            this.http.get<Server>(this.baseUrl + "api/v1/servers/" + params['id'] + "?fields=" +
                "name,description,isRunning," +
                "_links/self," +
                "_embedded(" +
                    "author(name,description,_links/self)," +
                    "simulations(name,description,_links/self)," +
                "presentations(name,description,_links/self))").subscribe(
                result => this.server = result,
                error => console.error(error)));
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
