import { EmbeddingResource } from '../../model/embedding-resource';

export abstract class RequestBuilder<T extends EmbeddingResource> {
    private params: string[] = [];

    protected constructor(private readonly defaultParams: string[]) {
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