import { Component } from '@angular/core'
import { Router } from '@angular/router'
import { ErrorHandlerService} from '../../service/error-handler-service'

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    private errors: string[] = [];

    constructor(
        private readonly errorHandler: ErrorHandlerService,
        private readonly router: Router) {
        errorHandler.errors.subscribe(errors => {
            this.errors = errors;
        });

        router.events.subscribe(_ => {
            //this.errors = [];
        });
    }
}
