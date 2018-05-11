import { Component, Inject, NgModule } from "@angular/core";
import { Http } from "@angular/http";
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: "simulations",
    templateUrl: "./simulation.component.html",
    styleUrls: ["./simulation.component.css"]
})
export class SimulationComponent {
    private id: number;
    private sub: any;
    public simulation: Simulation;

    constructor(private route: ActivatedRoute, private http: Http, @Inject("BASE_URL") private baseUrl: string) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id']; // (+) converts string 'id' to a number

            this.http.get(this.baseUrl + "api/v1/simulations/" + this.id + "?fields=" +
                "name,description," +
                "_links/self," +
                "_embedded(" +
                    "author(name,description,_links/self)," +
                    "references(name,description,_links/self)," +
                    "consumers(name,description,_links/self)," +
                    "presentations(name,description,_links/self))").subscribe(result => {
                        this.simulation = result.json() as Simulation;
            }, error => console.error(error));
        });
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
    references: Simulation[];
    consumers: Simulation[];
    presentations: Presentation[];
}

interface Author {
    name: string;
    description: string;
}

interface Presentation {
    name: string;
    description: string;
}

