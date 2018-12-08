import { ErrorHandler, Injectable } from '@angular/core';
import { Router, RoutesRecognized } from '@angular/router'
import { HttpErrorResponse } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { filter, pairwise } from 'rxjs/operators'


@Injectable()
export class ErrorHandlerService implements ErrorHandler {

    private subject = new Subject<string[]>();
    private previousRoute: string | undefined;

    public constructor(private readonly router: Router) {
        this.router.events.pipe(
                filter(e => e instanceof RoutesRecognized),
                pairwise())
            .subscribe((event: any[]) => this.previousRoute = event[0].urlAfterRedirects);
    }

    public handleError(error: any) {
        setTimeout(() => {
            if (error instanceof HttpErrorResponse) {
                this.subject.next([error.statusText]);
                this.router.navigateByUrl(this.previousRoute!);
            }
            else
                this.subject.next(error.message != undefined ? error.message : error.toString());

            throw error;
        });
    }

    public get errors(): Observable<string[]> {
        return this.subject;
    }
}