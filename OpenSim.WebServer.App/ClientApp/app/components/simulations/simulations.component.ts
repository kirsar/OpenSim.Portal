import { Component } from '@angular/core';
import { Simulation } from '../../model/simulation'
import { SimulationsService } from '../../service/simulations.service'
import { SimulationRequestBuilder } from '../../service/request-builder/simulation.builder'
import { PresentationRequestBuilder } from '../../service/request-builder/presentation.builder'

@Component({
    selector: 'simulations',
    templateUrl: './simulations.component.html',
    styleUrls: ['./simulations.component.css']
})
export class SimulationsComponent {
    private simulations: Simulation[] = [];

    constructor(private readonly service: SimulationsService) {
        this.querySimulations();
    }

    private querySimulations = () =>
        this.service.getAll(new SimulationRequestBuilder()
            .withAuthor()
            .withPresentations(new PresentationRequestBuilder().withAuthor())).subscribe(
                (simulations: Simulation[]) => this.simulations = simulations,
                (error: any) => console.error(error));

    public onSimulationCreated(simulation: Simulation) {
        //this.servers.push(server); // TODO just add result to list when servers in both components are compatible
        this.querySimulations();
    }
}

