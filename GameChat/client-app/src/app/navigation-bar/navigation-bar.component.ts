import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { UsersService } from '../services/users.service';
import { User } from '../models/user';

@Component({
    selector: 'app-navigation-bar',
    templateUrl: './navigation-bar.component.html',
    styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {

    get isLoggedIn() {
        return this.authService.isLoggedIn()
    }

    currentUser: User

    constructor(
        private authService: AuthenticationService,
        private router: Router,
        private userService: UsersService) {

        if (this.isLoggedIn) {
            userService.getCurrentUser().
                subscribe((user: User) => this.currentUser = user)
        }
    }

    ngOnInit() {
    }

    logout() {
        this.authService.logout()
        this.router.navigate([''])
    }
}
