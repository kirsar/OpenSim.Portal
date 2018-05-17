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
                "name,description," +
                "_links/self," +
                "_embedded(" +
                "author(name,_links/self)," +
                "references(name,description,_links/self,_embedded/author(name,_links/self))," +
                "consumers(name,description,_links/self,_embedded/author(name,_links/self))," +
                "presentations(name,description,_links/self,_embedded/author(name,_links/self)))").subscribe(result => {
                    this.simulations = result.json()._embedded.simulations as Simulation[];
            },
            error => console.error(error));
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
    name: string;
    description: string;
}

interface SimulationReference {
    name: string;
    description: string;
    _embedded: EmbeddedReference;
}

interface PresentationReference {
    name: string;
    description: string;
    _embedded: EmbeddedReference;
}

interface EmbeddedReference {
    author: Author;
}

