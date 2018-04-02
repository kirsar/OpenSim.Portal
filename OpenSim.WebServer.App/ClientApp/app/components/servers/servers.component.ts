import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'servers',
    templateUrl: './servers.component.html'
})
export class ServersComponent {
    public servers: Server[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/v1/server').subscribe(result => {
            this.servers = result.json() as Server[];
        }, error => console.error(error));
    }
}

interface Server {
    id: number;
    name: string;
    description: string;
    ownerId: number;
}
