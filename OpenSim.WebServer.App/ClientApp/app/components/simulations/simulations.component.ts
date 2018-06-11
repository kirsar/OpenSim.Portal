import { Component, Inject } from "@angular/core";
import { Http } from "@angular/http";

@Component({
    selector: "simulations",
    templateUrl: "./simulations.component.html",
    styleUrls: ["./simulations.component.css"]
})
export class SimulationsComponent {
    public simulations: Simulation[];

    constructor(http: Http, @Inject("BASE_URL") baseUrl: string) {
        http.get(baseUrl +
            "api/v1/simulations?fields=_embedded/simulations(" +
                "id,name,description," +
                "_links/self," +
                "_embedded(" +
                "author(id,name,_links/self)," +
                "references(id,name,description,_links/self,_embedded/author(id,name,_links/self))," +
                "consumers(id,name,description,_links/self,_embedded/author(id,name,_links/self))," +
                "presentations(id,name,description,_links/self,_embedded/author(id,name,_links/self)))").subscribe(result => {
                    this.simulations = result.json()._embedded.simulations as Simulation[];
            },
            error => console.error(error));
    }
}

interface Simulation {
    id: number;
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
    id: number;
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

