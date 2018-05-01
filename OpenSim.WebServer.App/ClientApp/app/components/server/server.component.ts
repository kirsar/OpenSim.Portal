import { Component, Inject, NgModule } from "@angular/core";
import { Http } from "@angular/http";
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: "servers",
    templateUrl: "./server.component.html",
    styleUrls: ["./server.component.css"]
})
export class ServerComponent {
    private id: number;
    private sub: any;
    public server: Server;

    constructor(private route: ActivatedRoute, private http: Http, @Inject("BASE_URL") private baseUrl: string) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id']; // (+) converts string 'id' to a number

            this.http.get(this.baseUrl + "api/v1/servers/" + this.id).subscribe(result => {
                this.server = result.json() as Server;
            }, error => console.error(error));
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}

interface Server {
    id: number;
    name: string;
    description: string;
    isRunning: boolean;
    simulations: Simulation[];
    presentations: Presentation[];
    selfLink: string;
}

interface Author {
    name: string;
}

interface Simulation {
    id: number;
    name: string;
    description: string;
}

interface Presentation {
    id: number;
    name: string;
    description: string;
}
