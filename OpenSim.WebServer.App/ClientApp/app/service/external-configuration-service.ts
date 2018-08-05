import { Inject, Injectable } from '@angular/core';
import { ExternalConfigurationHandlerInterface, ExternalConfiguration } from 'hal-4-angular';
import { HttpClient } from '@angular/common/http';

class ExternalConfigurationImpl implements ExternalConfiguration {
}

@Injectable()
export class ExternalConfigurationService implements ExternalConfigurationHandlerInterface {
    private configuration?: ExternalConfiguration;

    constructor(
        private readonly http: HttpClient,
        @Inject('BASE_URL') private readonly baseUrl: string) {
    }

    public getProxyUri(): string {
        return this.getRootUri();
    }

    public getRootUri(): string {
        return this.baseUrl + 'api/v1/';
    }

    public getHttp(): HttpClient {
        return this.http;
    }

    public getExternalConfiguration(): ExternalConfiguration {
        return new ExternalConfigurationImpl();
    }

    public setExternalConfiguration(externalConfiguration: ExternalConfiguration) {
    }

    public deserialize(): any {
    }

    public serialize(): any {
    }
}


