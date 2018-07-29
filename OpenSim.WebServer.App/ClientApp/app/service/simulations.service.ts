import { Injectable, Injector } from '@angular/core'
import { Observable } from 'rxjs';
import { RestService, HalOptions } from 'hal-4-angular'
import { Simulation } from "../model/simulation"
import { SimulationRequestBuilder } from '../service/request-builder/simulation.builder'

@Injectable()
export class SimulationsService {
    private readonly service: RestService<Simulation>;
    private readonly resource: string = 'simulations';

    constructor(injector: Injector) {
        this.service = new RestService<Simulation>(Simulation, this.resource, injector);
    }

    public getAll(builder?: SimulationRequestBuilder): Observable<Simulation[]> {
        const options: HalOptions = builder != undefined ? {
                params: [
                    {
                        key: 'fields',
                        value: `_embedded/${this.resource}(${builder.build()})`
                    }
                ]
            } : {};

        return this.service.getAll(options);
    }
}