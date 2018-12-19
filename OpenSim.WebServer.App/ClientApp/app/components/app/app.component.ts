import { Component } from '@angular/core'
import { ErrorHandlerService} from '../../service/error-handler-service'

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    private errors: string[] = [];

    constructor(/*private readonly errorHandler: ErrorHandlerService*/) {
        //errorHandler.errors.subscribe(errors => this.errors = errors);
    }
}
