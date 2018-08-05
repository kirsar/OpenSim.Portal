import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
    selector: 'new-server-form',
    templateUrl: './new-server.component.html',
    styleUrls: ['./new-server.component.css']
})
export class NewServerFormComponent {
    public simulations: ISimulation[] = [];

    constructor(private readonly http: HttpClient, @Inject('BASE_URL') private readonly  baseUrl: string) {
        http.get(baseUrl + 'api/v1/simulations', { responseType: 'text' }).subscribe(
            res => this.simulations = JSON.parse(res)._embedded.simulations as ISimulation[],
            error => console.error(error));
    }

    @Output() public serverCreated = new EventEmitter<Server>();

    private server = new Server('New Server 1');

    //get isSimulationSelected() {
    //    return this.simulations.filter(s => s.isSelected).length > 0;
    //}

    private onCreate() {
        this.simulations.filter(sim => sim.isSelected).forEach(
            sim => this.server._links.simulations.push(new SimulationLink(sim.id)));

        const httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/hal+json' })
        };

        this.http.post(this.baseUrl + 'api/v1/servers', this.server, httpOptions).subscribe(
            res => {
                this.server = new Server('New Server 1');
                this.simulations.forEach(s => s.isSelected = false);
                this.serverCreated.emit(res as Server);
            });
    }
}

interface ISimulation {
    id: number;
    name: string;
    description: string;
    isSelected: boolean;
}

class Server {
    constructor(public name: string)
    {
        this._links = new Links();
    }

    description?: string;
    _links: Links;
}

class Links {
    constructor() {
        this.simulations = [];
    }

    simulations: SimulationLink[];
}

class SimulationLink {
    constructor(id: number) {
        this.href = `\\simulations\\${id}`;
    }

    public href: string;
}
