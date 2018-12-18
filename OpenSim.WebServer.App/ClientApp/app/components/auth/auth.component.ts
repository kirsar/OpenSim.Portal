import { Component } from '@angular/core';
import { AuthenticationService } from '../../service/authentication-service'

@Component({
    selector: 'auth',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.css']
})
export class AuthComponent {
    private username?: string;
    private password?: string;
    private showError = false;

    constructor(private readonly auth: AuthenticationService) {
    }

    login() {
        this.showError = false;
        this.auth.login(this.username!, this.password!).subscribe(success => {
        this.showError = !success;
    }); }
}

