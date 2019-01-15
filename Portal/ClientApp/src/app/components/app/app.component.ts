import { Component } from '@angular/core'
import { ErrorHandlerService } from '../../service/error-handler-service'
import { NavigationService } from '../../service/navigation-service'

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    public errors: string[] = [];

    // TODO figure out APP_INITIALIZER and resolving cycles with Injector
    // for now, claiming Navigation here to make it collect data from start of app
    constructor(navigation: NavigationService) {
        //private readonly errorHandler: ErrorHandlerService*/) {
        //errorHandler.errors.subscribe(errors => this.errors = errors);
        navigation.init();
    }
}
