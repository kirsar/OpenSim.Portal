import { Component, Inject } from "@angular/core";
import { HttpClient, } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: "presentations",
    templateUrl: "./presentation.component.html",
    styleUrls: ["./presentation.component.css"]
})
export class PresentationComponent {
    private sub: any;
    public presentation?: Presentation;

    constructor(private readonly route: ActivatedRoute, private readonly http: HttpClient, @Inject("BASE_URL") private readonly baseUrl: string) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params =>
            this.http.get<Presentation>(this.baseUrl + "api/v1/presentations/" + params['id'] + "?fields=" +
                "name,description," +
                "_links/self," +
                "_embedded(" +
                    "author(id, name,description,_links/self)," +
                "simulations(id, name,description,_links/self,_embedded/author(id, name,_links/self)))").subscribe(
                result => this.presentation = result,
                error => console.error(error)));
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}

interface Presentation {
    name: string;
    description: string;
    _embedded: Embedded;
}

interface Embedded {
    author: Author;
    references: SimulationReference[];
}

interface SimulationReference {
    id: number;
    name: string;
    description: string;
    _embedded: EmbeddedReference;
}

interface EmbeddedReference {
    author: Author;
}

interface Author {
    id: number;
    name: string;
    description: string;
}

