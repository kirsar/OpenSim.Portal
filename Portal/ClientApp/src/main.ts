import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

function getBaseUrlFromAsp() {
  return document.getElementsByTagName('base')[0].href;
}

export function getBaseUrl() {
    const baseUrl = environment.production ? environment.apiHost : getBaseUrlFromAsp();

    console.log(`base url for api: ${baseUrl}`);

    return baseUrl;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.error(err));
