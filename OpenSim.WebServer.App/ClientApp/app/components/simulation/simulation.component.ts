import { Component, Inject } from "@angular/core";
import { HttpClient, } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: "simulations",
    templateUrl: "./simulation.component.html",
    styleUrls: ["./simulation.component.css"]
})
export class SimulationComponent {
    private sub: any;
    public simulation?: Simulation;

    constructor(
        private readonly route: ActivatedRoute,
        private readonly http: HttpClient, @Inject("BASE_URL")
        private readonly baseUrl: string) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params =>
            this.http.get<Simulation>(this.baseUrl + "api/v1/simulations/" + params['id'] + "?fields=" +
                "name,description," +
                "_links/self," +
                "_embedded(" +
                    "author(id,name,description,_links/self)," +
                    "references(id,name,description,_links/self,_embedded/author(id,name,_links/self))," +
                    "consumers(id,name,description,_links/self,_embedded/author(id,name,_links/self))," +
                "presentations(id,name,description,_links/self,_embedded/author(id,name,_links/self)))").subscribe(
                result => this.simulation = result,
                error => console.error(error)));
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}

interface Simulation {
    name: string;
    description: string;
    _embedded: Embedded;
}

interface Embedded {
    author: Author;
    references: SimulationReference[];
    consumers: SimulationReference[];
    presentations: PresentationReference[];
}

interface Author {
    id : number;
    name: string;
    description: string;
}

interface SimulationReference {
    id: number;
    name: string;
    description: string;
    _embedded: EmbeddedReference;
}

interface PresentationReference {
    id: number;
    name: string;
    description: string;
    _embedded: EmbeddedReference;
}

interface EmbeddedReference {
    author: Author;
}

