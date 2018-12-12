import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable()
export class AuthenticationService {
    constructor(private readonly router: Router) {
    }

    public isAuthenticated: boolean = false;
    
    login(name: string, password: string, callbackUrl: string): Observable<boolean> {
        this.isAuthenticated = false;
        //return this.repo.login()
        //    .map(response => {
        //        if (response.ok) {
        //            this.authenticated = true;
        //            this.password = null;
        //            this.router.navigateByUrl(this.callbackUrl);
        //        }
        //        return this.authenticated;
        //    })
        //    .catch(e => {
        //        this.authenticated = false;
        //        return Observable.of(false);
        //    });

        return of(true);
    }

    logout() {
        this.isAuthenticated = false;
        //this.repo.logout();
        this.router.navigateByUrl('/login');
    }
}