import { Component, Output, EventEmitter } from '@angular/core';
import { Server } from '../../model/server';
import { ServersService } from '../../service/servers.service';
import { SimulationsService } from '../../service/simulations.service';
import { SimulationItem } from './simulation-item';
import { PresentationsService } from '../../service/presentations.service';
import { PresentationItem } from './presentation-item';
import { Link } from '../../model/link';

@Component({
    selector: 'new-server-form',
    templateUrl: './new-server.component.html',
    styleUrls: ['./new-server.component.css']
})
export class NewServerFormComponent {
    constructor(
        private readonly serversService: ServersService,
        private readonly simulationsService: SimulationsService,
        private readonly presentationsService: PresentationsService)
    {
        simulationsService.getAll().subscribe(
            result => this.simulations = result.map(s => new SimulationItem(s)),
            error => console.error(error));

        presentationsService.getAll().subscribe(
            result => this.presentations = result.map(p => new PresentationItem(p)),
            error => console.error(error));
    }

    private server = this.buildDefaultServer();

    public simulations: SimulationItem[] = [];
    public presentations: PresentationItem[] = [];
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
            s => this.server.addSimulation(s.id));

        this.presentations.filter(s => s.isSelected).forEach(
            p => this.server.addPresentation(p.id));

        // TODO use current user as author
        this.serversService.post(this.server).subscribe(
            res => {
                this.buildDefaultServer();
                this.simulations.forEach(s => s.isSelected = false);
                this.presentations.forEach(s => s.isSelected = false);
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
