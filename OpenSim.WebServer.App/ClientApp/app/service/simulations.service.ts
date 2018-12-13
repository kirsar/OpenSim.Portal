import { Inject, Injectable, Injector } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { ExternalConfigurationHandlerInterface } from 'hal-4-angular';
import { Simulation } from '../model/simulation'
import { ApiService } from './api-service'

@Injectable()
export class SimulationsService extends ApiService<Simulation> {

    constructor(
        injector: Injector,
        private readonly http: HttpClient,
        @Inject('ExternalConfigurationService') private readonly  externalConfigurationService: ExternalConfigurationHandlerInterface) {
        super(Simulation, 'simulations', injector);
    }

    public upload(content: any): Observable<Object> {
        const options = { headers: new HttpHeaders({'Content-Type': 'application/hal+json'}) };

        return this.http.post(`${this.externalConfigurationService.getRootUri()}\\${this.resource}`, content, options);
    }
}