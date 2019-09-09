import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login-form',
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

    loginForm: FormGroup;
    invalidCredentials: boolean;
    unexpectedError: boolean;

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthenticationService,
        private router: Router) {

        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        })
    }

    get username() {
        return this.loginForm.controls.username
    }

    get password() {
        return this.loginForm.controls.password
    }

    onSubmit() {
        if (!this.loginForm.valid)
            return;

        this.authService.login(this.loginForm.controls.username.value, this.loginForm.controls.password.value).
            subscribe(
                (data: any) => {
                    if (data && data.token)
                        localStorage.setItem('currentUser', data['token'])

                    this.router.navigate(['/user'])
                },
                error => {
                    if (error.status == 401)
                        this.invalidCredentials = true;
                    else
                        this.unexpectedError = true;
                })
    }

    ngOnInit() {

    }

}
