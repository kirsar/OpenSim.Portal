import { Injectable, Injector } from '@angular/core'
import { Simulation } from '../model/simulation'
import { ApiService } from './api-service'

@Injectable()
export class SimulationsService extends ApiService<Simulation> {

    constructor(injector: Injector) {
        super(Simulation, 'simulations', injector);
    }
}