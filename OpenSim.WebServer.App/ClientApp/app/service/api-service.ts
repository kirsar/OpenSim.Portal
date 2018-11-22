import { Injector } from '@angular/core'
import { Observable } from 'rxjs';
import { RestService, HalOptions, HalParam } from 'hal-4-angular'
import { EmbeddingResource } from '../model/embedding-resource';
import { RequestBuilder } from './request-builder/request-builder-interface'
import { map } from 'rxjs/operators'

// TODO get type from T
export abstract class ApiService<T extends EmbeddingResource> {
    private readonly service: RestService<T>;

    protected constructor(type: { new(): T; }, protected  readonly resource: string, injector: Injector) {
        this.service = new RestService<T>(type, resource, injector);
    }

    public getAll(builder?: RequestBuilder<T>): Observable<T[]> {
        return this.service.getAll(this.buildOptions(builder));
    }
    
    // TODO make PR in fork hal-4-angular to support options in get(id)
    public get(id: number, builder?: RequestBuilder<T>): Observable<T | undefined> {
        return this.service.get(id, this.buildResourceParams(builder));
    }

    public post(resource: T): Observable<Observable<never> | T> {
        return this.service.create(resource);
    }

    private buildResourceCollectionParams(builder?: RequestBuilder<T>): HalParam[] {
        return builder != undefined
            ? [
                {
                    key: 'fields',
                    value: `_embedded/${this.resource}(${builder.build()})`
                }
            ]
            : [];
    }

    private buildResourceParams(builder?: RequestBuilder<T>): HalParam[] {
        return builder != undefined
            ? [
                {
                    key: 'fields',
                    value: `${builder.build()}`
                }
            ]
            : [];
    }

    private buildOptions(builder?: RequestBuilder<T>): HalOptions {
        return builder != undefined
            ? { params: this.buildResourceCollectionParams(builder) }
            : {};
    }
}