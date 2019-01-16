import { Component } from '@angular/core';
import { AuthenticationService } from '../../service/authentication-service'

@Component({
    selector: 'auth-form',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.css']
})
export class AuthComponent {
    public username?: string;
    public password?: string;
    public showError = false;

    constructor(private readonly auth: AuthenticationService) {
    }

    login() {
        this.showError = false;
        this.auth.login(this.username!, this.password!).subscribe(success => {
            this.showError = !success;
        });
    }
}

