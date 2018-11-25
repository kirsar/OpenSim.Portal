import { Injector } from '@angular/core'
import { Observable } from 'rxjs';
import { RestService, HalOptions, HalParam } from 'hal-4-angular'
import { EmbeddingResource } from '../model/embedding-resource';
import { Resource } from 'hal-4-angular';
import { RequestBuilder } from './request-builder/request-builder-interface'
import { HalOptionsBuilder } from './hal-options-builder'
import { map } from 'rxjs/operators'

// TODO get type from T
export abstract class ApiService<T extends Resource> {
    private readonly service: RestService<T>;

    protected constructor(type: { new(): T; }, protected  readonly resource: string, injector: Injector) {
        this.service = new RestService<T>(type, resource, injector);
    }

    public getAll(builder?: RequestBuilder<T>): Observable<T[]> {
        return this.service.getAll(HalOptionsBuilder.buildOptionsForCollection(this.resource, builder));
    }
    
    public get(id: number, builder?: RequestBuilder<T>): Observable<T | undefined> {
        return this.service.get(id, HalOptionsBuilder.buildResourceParams(builder));
    }

    public post(resource: T): Observable<Observable<never> | T> {
        return this.service.create(resource);
    }
}