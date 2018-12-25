import { Injectable, Inject } from '@angular/core'
import { Router } from '@angular/router'
import { Observable, of } from 'rxjs'
import { HttpClient, HttpHeaders } from '@angular/common/http/'
import { ExternalConfigurationHandlerInterface } from 'angular4-hal'
import { NavigationService } from './navigation-service'
import { map, catchError } from 'rxjs/operators'

@Injectable()
export class AuthenticationService {
    constructor(
        private readonly http: HttpClient,
        private readonly router: Router,
        private readonly navigation: NavigationService,
        @Inject('ExternalConfigurationService') private readonly externalConfigurationService: ExternalConfigurationHandlerInterface) {
    }

    public isAuthenticated: boolean = false;

    public login(name: string, password: string, callbackUrl?: string): Observable<boolean> {
        this.isAuthenticated = false;
        const self = this;
        return this.post(name, password).pipe(
            map((result: any) => {
                if (result != undefined) {
                    this.isAuthenticated = true;

                    callbackUrl = callbackUrl != undefined ? callbackUrl : self.navigation.previousRoute;
                    if (callbackUrl != undefined)
                        self.router.navigateByUrl(callbackUrl);
                }

                return this.isAuthenticated;
            }),
            catchError(_ => {
                this.isAuthenticated = false;
                return of(false);
            }));
    }

    public logout() {
        this.isAuthenticated = false;
        this.post('', '');
    }

    private post(name: string, password: string): Observable<Object> {
        const options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

        return this.http.post(
            `${this.externalConfigurationService.getRootUri()}authentication`,
            { username: name, password: password },
            options);
    }
}
