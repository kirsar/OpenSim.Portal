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
        @Inject("BASE_URL") private readonly baseUrl: string) {
    }

    getProxyUri(): string {
        return this.baseUrl + "api/v1/";
    }

    getRootUri(): string {
        return this.baseUrl + "api/v1/";
    }

    getHttp(): HttpClient {
        return this.http;
    }

    getExternalConfiguration(): ExternalConfiguration {
        return new ExternalConfigurationImpl();
    }

    setExternalConfiguration(externalConfiguration: ExternalConfiguration) {
    }

    deserialize(): any {
    }

    serialize(): any {
    }
}


