import { Component, Inject } from "@angular/core";
import { HttpClient, } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: "simulations",
    templateUrl: "./simulation.component.html",
    styleUrls: ["./simulation.component.css"]
})
export class SimulationComponent {
    private id?: number;
    private sub: any;
    public simulation?: Simulation;

    constructor(private route: ActivatedRoute, private http: HttpClient, @Inject("BASE_URL") private baseUrl: string) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id']; // (+) converts string 'id' to a number

            this.http.get(this.baseUrl + "api/v1/simulations/" + this.id + "?fields=" +
                "name,description," +
                "_links/self," +
                "_embedded(" +
                    "author(id,name,description,_links/self)," +
                    "references(id,name,description,_links/self,_embedded/author(id,name,_links/self))," +
                    "consumers(id,name,description,_links/self,_embedded/author(id,name,_links/self))," +
                    "presentations(id,name,description,_links/self,_embedded/author(id,name,_links/self)))").subscribe(result => {
                        this.simulation = result/*.json()*/ as Simulation;
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

