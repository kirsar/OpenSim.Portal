import { HalOptions, HalParam, Resource } from 'angular4-hal'
import { RequestBuilder } from './request-builder/request-builder-interface'

export abstract class HalOptionsBuilder {
    public static buildParamsForResource<T extends Resource>(builder?: RequestBuilder<T>): HalParam[] {
        return builder != undefined
            ? [
                {
                    key: 'fields',
                    value: `${builder.build()}`
                }
            ]
            : [];
    }

    public static buildParamsForCollection<T extends Resource>(resource: string, builder?: RequestBuilder<T>): HalParam[] {
        return builder != undefined
            ? [
                {
                    key: 'fields',
                    value: `_embedded/${resource}(${builder.build()})`
                }
            ]
            : [];
    }

    public static buildOptionsForResource<T extends Resource>(builder?: RequestBuilder<T>): HalOptions {
        return builder != undefined
            ? { params: this.buildParamsForResource(builder) }
            : {};
    }

    public static buildOptionsForCollection<T extends Resource>(resource: string, builder?: RequestBuilder<T>): HalOptions {
        return builder != undefined
            ? { params: this.buildParamsForCollection(resource, builder) }
            : {};
    }
}
