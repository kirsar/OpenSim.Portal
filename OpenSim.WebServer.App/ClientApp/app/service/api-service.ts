import { Injector } from '@angular/core'
import { Observable } from 'rxjs';
import { RestService, HalOptions } from 'hal-4-angular'
import { EmbeddingResource } from '../model/embedding-resource';
import { RequestBuilder } from './request-builder/request-builder-interface'
import { map } from 'rxjs/operators'

// TODO get type from T
export abstract class ApiService<T extends EmbeddingResource> {
    private readonly service: RestService<T>;

    protected constructor(type: { new(): T; }, private readonly resource: string, injector: Injector) {
        this.service = new RestService<T>(type, resource, injector);
    }

    public getAll(builder?: RequestBuilder<T>): Observable<T[]> {
        return this.service.getAll(this.buildOptions(builder));
    }
    
    // TODO make PR in fork hal-4-angular to support options in get(id)
    public get(id: number, builder?: RequestBuilder<T>): Observable<T | undefined> {
        return this.getAll(builder).pipe(map(items => items ? items.find(item => item.id == id) : undefined));
    }

    public post(resource: T): Observable<Observable<never> | T> {
        return this.service.create(resource);
    }

    private buildOptions(builder?: RequestBuilder<T>): HalOptions {
        return builder != undefined ? {
            params: [
                {
                    key: 'fields',
                    value: `_embedded/${this.resource}(${builder.build()})`
                }
            ]
        } : {};
    }
}