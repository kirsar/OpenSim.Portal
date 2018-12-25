import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router'
import { Server } from '../../model/server';
import { ServersService } from '../../service/servers.service';
import { SimulationsService } from '../../service/simulations.service';
import { PresentationsService } from '../../service/presentations.service';
import { AuthenticationService } from '../../service/authentication-service'
import { ComponentCollection } from './components-collection'

@Component({
    selector: 'new-server-form',
    templateUrl: './new-server.component.html',
    styleUrls: ['./new-server.component.css']
})
export class NewServerFormComponent {
    constructor(
        private readonly router: Router,
        private readonly serversService: ServersService,
        private readonly simulationsService: SimulationsService,
        private readonly presentationsService: PresentationsService,
        private readonly authenticationService: AuthenticationService) {
        this.components = new ComponentCollection(simulationsService, presentationsService);
    }

    private server = this.buildDefaultServer();
    public readonly components: ComponentCollection;
    public get isAuthenticated(): boolean { return this.authenticationService.isAuthenticated; }

    @Output() public serverCreated = new EventEmitter<Server>();

    public isInvalid(): boolean {
        if (!this.isAuthenticated)
            return false;

        if (!this.components.hasSelection)
            return true;
        if (this.server.name == undefined)
            return true;
        if (this.server.name!.length === 0)
            return true;

        return false;
    }

    private onCreate() {
        if (!this.checkAuth())
            return;

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

    private checkAuth() {
        if (this.isAuthenticated)
            return true;

        this.router.navigateByUrl('/login');
        return false;
    }
   
    // construct full object before assignment, change detection can occur 
    private buildDefaultServer() : Server {
        const newServer = new Server();
        newServer.name = 'New Server 1';
        this.server = newServer; 
        return this.server;
    }
}
