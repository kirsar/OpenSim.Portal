import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router'
import { HttpErrorResponse } from '@angular/common/http';
import { NavigationService } from './navigation-service'
import { Observable, Subject } from 'rxjs';

@Injectable()
export class ErrorHandlerService {

    private subject = new Subject<string[]>();
    
    public constructor(private readonly injector: Injector) {
    }

    public handleError(error: any) {
        setTimeout(() => {
            if (error instanceof HttpErrorResponse) {
                this.subject.next([error.statusText]);

                const previousRoute = this.injector.get(NavigationService).previousRoute;
                if (previousRoute != undefined)
                    this.injector.get(Router).navigateByUrl('/servers');
            }
            else
                this.subject.next(error.message != undefined ? error.message : error.toString());

            //throw error;
        });
    }

    public get errors(): Observable<string[]> {
        return this.subject;
    }
}