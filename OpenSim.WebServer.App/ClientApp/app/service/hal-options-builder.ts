import { Injector } from '@angular/core'
import { Observable } from 'rxjs';
import { RestService, HalOptions, HalParam } from 'hal-4-angular'
import { EmbeddingResource } from '../model/embedding-resource';
import { Resource } from 'hal-4-angular';
import { RequestBuilder } from './request-builder/request-builder-interface'
import { map } from 'rxjs/operators'

export abstract class HalOptionsBuilder {
    private static  buildResourceCollectionParams<T extends Resource>(resource: string, builder?: RequestBuilder<T>): HalParam[] {
        return builder != undefined
            ? [
                {
                    key: 'fields',
                    value: `_embedded/${resource}(${builder.build()})`
                }
            ]
            : [];
    }

    public static buildResourceParams<T extends Resource>(builder?: RequestBuilder<T>): HalParam[] {
        return builder != undefined
            ? [
                {
                    key: 'fields',
                    value: `${builder.build()}`
                }
            ]
            : [];
    }

    public static buildOptionsForCollection<T extends Resource>(resource: string, builder?: RequestBuilder<T>): HalOptions {
        return builder != undefined
            ? { params: this.buildResourceCollectionParams(resource, builder) }
            : {};
    }

    public static buildOptionsForResource<T extends Resource>(builder?: RequestBuilder<T>): HalOptions {
        return builder != undefined
            ? { params: this.buildResourceParams(builder) }
            : {};
    }
}