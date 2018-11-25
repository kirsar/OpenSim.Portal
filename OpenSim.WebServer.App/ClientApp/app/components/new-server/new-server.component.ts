import { Component, Output, EventEmitter } from '@angular/core';
import { Server } from '../../model/server';
import { ServersService } from '../../service/servers.service';
import { SimulationsService } from '../../service/simulations.service';
import { PresentationsService } from '../../service/presentations.service';
import { ComponentCollection } from './components-collection'

@Component({
    selector: 'new-server-form',
    templateUrl: './new-server.component.html',
    styleUrls: ['./new-server.component.css']
})
export class NewServerFormComponent {
    constructor(
        private readonly serversService: ServersService,
        simulationsService: SimulationsService,
        presentationsService: PresentationsService) {
        this.components = new ComponentCollection(simulationsService, presentationsService);
    }

    private server = this.buildDefaultServer();
    public readonly components: ComponentCollection;

    @Output() public serverCreated = new EventEmitter<Server>();

    public isInvalid(): boolean {
        if (!this.components.hasSelection)
            return true;
        if (this.server.name == undefined)
            return true;
        if (this.server.name!.length === 0)
            return true;

        return false;
    }

    private onCreate() {
        this.components.simulations.filter(s => s.isSelected).forEach(
            s => this.server.addSimulation(s.simulation));
         
        this.components.presentations.filter(s => s.isSelected).forEach(
            p => this.server.addPresentation(p.presentation));

        const newServer = this.server;
        this.buildDefaultServer();

        this.serversService.post(newServer).subscribe(
            res => {
                this.components.setDefaultSelection();
                if (res instanceof Server)
                    this.serverCreated.emit(res);
            });
    }

    // construct full object before assignment, change detection can occur 
    private buildDefaultServer() : Server {
        const newServer = new Server();
        newServer.name = 'New Server 1';
        this.server = newServer; 
        return this.server;
    }
}
