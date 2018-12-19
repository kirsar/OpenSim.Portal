import { Injectable } from '@angular/core'
import { Router, RoutesRecognized } from '@angular/router'
import { filter, pairwise } from 'rxjs/operators'

@Injectable()
export class NavigationService {
    constructor(
        private readonly router: Router) {
        this.router.events.pipe(
                filter(e => e instanceof RoutesRecognized),
                pairwise())
            .subscribe((event: any[]) => this.previousRoute = event[0].urlAfterRedirects);
    }

    public previousRoute: string | undefined;
}