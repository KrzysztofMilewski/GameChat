import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { passwordMatchValidator } from '../helpers/PasswordsMatchValidator';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-register-form',
    templateUrl: './register-form.component.html',
    styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {

    registrationForm: FormGroup
    errorMessage: string

    get username() {
        return this.registrationForm.controls.username
    }

    get password() {
        return this.registrationForm.controls.password
    }

    get confirmPassword() {
        return this.registrationForm.controls.confirmPassword
    }

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthenticationService,
        private router: Router) {

        this.registrationForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.minLength(6)]],
            password: ['', [Validators.required, Validators.pattern("^[a-zA-Z0-9\\[\\]\{\}\:\;\'\"\/\?\,\~\`\|\<\>\\\\!@#\$%\^\&*\)\(+=._-]{8,16}$")]],
            confirmPassword: ['', [Validators.required, Validators.pattern("^[a-zA-Z0-9\\[\\]\{\}\:\;\'\"\/\?\,\~\`\|\<\>\\\\!@#\$%\^\&*\)\(+=._-]{8,16}$")]]
        }, { validators: [passwordMatchValidator] })
    }

    onSubmit() {
        if (this.registrationForm.invalid)
            return

        this.authService.register(this.username.value, this.password.value).
            subscribe(data => {
                //TODO add some visual indication (toast or sth similar) to indicate that account has been created successfully
                this.router.navigate(['/login'])
            },
                error => {
                    this.errorMessage = error.error
            })
    }

    ngOnInit() {
    }

}
