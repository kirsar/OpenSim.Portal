import { Resource, } from 'hal-4-angular';

export abstract class RequestBuilder<T extends Resource> {
    private params: string[] = [];

    protected constructor(private readonly defaultParams: string[]) {
        defaultParams.push('id');
        defaultParams.push('_links');
    }

    protected addRelation<TResource extends Resource, TBuilder extends RequestBuilder<TResource>>(
        builderFactory: { new(): TBuilder; }, relation: string, builder?: TBuilder) {
            this.addParam(`${relation}(${(builder ? builder : new builderFactory()).build()})`);
    }

    protected addParam(param: string) {
        this.params.push(param);
    }

    public build(): string {
        let result = this.defaultParams.join(',');

        if (this.params.length > 0)
            result += `,_embedded(${this.params.join(',')})`;

        return result;
    }
}