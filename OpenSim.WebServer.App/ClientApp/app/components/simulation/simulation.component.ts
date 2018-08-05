import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Simulation } from '../../model/simulation';
import { SimulationsService } from '../../service/simulations.service';
import { SimulationRequestBuilder } from '../../service/request-builder/simulation.builder'
import { PresentationRequestBuilder } from '../../service/request-builder/presentation.builder'

@Component({
    selector: 'simulations',
    templateUrl: './simulation.component.html',
    styleUrls: ['./simulation.component.css']
})
export class SimulationComponent {
    private subscription: any;
    public simulation?: Simulation | undefined;

    constructor(
        private readonly route: ActivatedRoute,
        private readonly service: SimulationsService)
    { }

    private ngOnInit() {
        this.subscription = this.route.params.subscribe(params =>
            this.service.get(params['id'], new SimulationRequestBuilder()
                .withAuthor()
                .withReferences(new SimulationRequestBuilder().withAuthor())
                .withConsumers(new SimulationRequestBuilder().withAuthor())
                .withPresentations(new PresentationRequestBuilder().withAuthor())).subscribe(
                    (result: Simulation | undefined) => this.simulation = result,
                    (error: any) => console.error(error)));
    }

    private ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}

