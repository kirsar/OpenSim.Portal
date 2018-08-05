import { Injectable, Injector } from '@angular/core'
import { Presentation } from '../model/presentation'
import { ApiService } from './api-service'

@Injectable()
export class PresentationsService extends ApiService<Presentation> {

    constructor(injector: Injector) {
        super(Presentation, 'presentations', injector);
    }
}