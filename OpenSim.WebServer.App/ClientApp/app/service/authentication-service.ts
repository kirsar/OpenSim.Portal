import { Injectable, Inject } from '@angular/core'
import { Router } from '@angular/router'
import { Observable, of } from 'rxjs'
import { HttpClient, HttpResponseBase, HttpHeaders } from '@angular/common/http/'
import { ExternalConfigurationHandlerInterface } from 'hal-4-angular'
import { map, catchError } from 'rxjs/operators'

@Injectable()
export class AuthenticationService {
    constructor(
        private readonly router: Router,
        private readonly http: HttpClient,
        @Inject('ExternalConfigurationService') private readonly externalConfigurationService: ExternalConfigurationHandlerInterface) {
    }

    public isAuthenticated: boolean = false;

    public login(name: string, password: string, callbackUrl?: string): Observable<boolean> {
        this.isAuthenticated = false;
        return this.post(name, password).pipe(
            map((result: any) => {
                if (result != undefined) {
                    this.isAuthenticated = true;

                    if (callbackUrl != undefined)
                        this.router.navigateByUrl(callbackUrl);
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
        //this.router.navigateByUrl('/login');
    }

    private post(name: string, password: string): Observable<Object> {
        const options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

        return this.http.post(
            `${this.externalConfigurationService.getRootUri()}authentication`,
            { username: name, password: password },
            options);
    }
}