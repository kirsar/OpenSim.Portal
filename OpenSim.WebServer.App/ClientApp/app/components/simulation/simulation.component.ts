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

            this.http.get(this.baseUrl + "api/v1/simulations/" + this.id).subscribe(result => {
                this.simulation = result.json() as Simulation;
            }, error => console.error(error));
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}

interface Simulation {
    id: number;
    name: string;
    description: string;
    author: Author;
    references: Simulation[];
    //consumers: Simulation[];
    presentations: Presentation[];
}

interface Presentation {
    id: number;
    name: string;
    author: Author;
}

interface Author {
    name: string;
}

