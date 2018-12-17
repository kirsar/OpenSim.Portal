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

    constructor(private readonly auth: AuthenticationService) {
    }

    login() { this.auth.login(this.username!, this.password!).subscribe(_ => {}); }
}

