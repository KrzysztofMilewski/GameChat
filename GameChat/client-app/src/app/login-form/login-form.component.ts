import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { AuthenticationService } from '../services/authentication.service';

@Component({
    selector: 'app-login-form',
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

    loginForm: FormGroup;

    constructor(private formBuilder: FormBuilder, private authService: AuthenticationService) {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        })
    }

    onSubmit() {
        if (!this.loginForm.valid)
            return;

        this.authService.login(this.loginForm.controls.username.value, this.loginForm.controls.password.value).
            subscribe(
                (data: any) => {
                    if (data && data.token)
                        localStorage.setItem('currentUser', data['token'])
                },
                error => {
                    //TODO Change it to some kind of visual indication that credentials were invalid
                    console.log(error)
                })
    }

    ngOnInit() {
    }

}
