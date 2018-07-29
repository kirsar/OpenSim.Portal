import { Resource } from 'hal-4-angular'

export abstract  class RequestBuilder<T extends Resource> {
    private params: string[] = [];

    protected constructor(private readonly defaultParams: string[]) {
    }

    protected addParam(param: string) {
        this.params.push(param);
    }

    build(): string {
        let result = this.defaultParams.join(",");

        if (this.params.length > 0)
            result += `,_embedded(${this.params.join(",")})`;

        return result;
    }
}