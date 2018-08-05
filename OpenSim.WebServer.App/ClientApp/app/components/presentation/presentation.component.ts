import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Presentation } from '../../model/presentation';
import { PresentationsService } from '../../service/presentations.service';
import { PresentationRequestBuilder } from '../../service/request-builder/presentation.builder'
import { SimulationRequestBuilder } from '../../service/request-builder/simulation.builder'

@Component({
    selector: 'presentations',
    templateUrl: './presentation.component.html',
    styleUrls: ['./presentation.component.css']
})
export class PresentationComponent {
    private subscription: any;
    public presentation?: Presentation;

    constructor(
        private readonly route: ActivatedRoute,
        private readonly service: PresentationsService) { }

    private ngOnInit() {
        this.subscription = this.route.params.subscribe(params =>
            this.service.get(params['id'], new PresentationRequestBuilder()
                .withAuthor()
                .withSimulations(new SimulationRequestBuilder().withAuthor())).subscribe(
                    (result: Presentation | undefined) => this.presentation = result,
                    (error: any) => console.error(error)));
    }

    private ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}