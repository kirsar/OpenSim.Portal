import { Component, Output, EventEmitter } from '@angular/core';
import { Server } from '../../model/server';
import { SimulationsService } from '../../service/simulations.service';
import { ServersService } from '../../service/servers.service';
import { SimulationItem } from './simulation-item';
import { Link } from '../../model/link';

@Component({
    selector: 'new-server-form',
    templateUrl: './new-server.component.html',
    styleUrls: ['./new-server.component.css']
})
export class NewServerFormComponent {
    constructor(
        private readonly serversService: ServersService,
        private readonly simulationsService: SimulationsService)
    {
        simulationsService.getAll().subscribe(
            result => this.simulations = result.map(s => new SimulationItem(s)),
            error => console.error(error));
    }

    private server = this.buildDefaultServer();

    public simulations: SimulationItem[] = [];
    @Output() public serverCreated = new EventEmitter<Server>();

    public isInvalid(): boolean {
        if (this.simulations.filter(s => s.isSelected).length === 0)
            return true;
        if (this.server.name!.length === 0)
            return true;

        return false;
    }

    private onCreate() {
        this.simulations.filter(s => s.isSelected).forEach(
            sim => this.server._links.simulations.push(new Link('simulations', sim.simulation.id)));

        // TODO use current user as author
        this.serversService.post(this.server).subscribe(
            res => {
                this.buildDefaultServer();
                this.simulations.forEach(s => s.isSelected = false);
                if (res instanceof Server)
                    this.serverCreated.emit(res);
            });
    }

    private buildDefaultServer() : Server {
        this.server = new Server();
        this.server.name = 'New Server 1';
        return this.server;
    }
}
