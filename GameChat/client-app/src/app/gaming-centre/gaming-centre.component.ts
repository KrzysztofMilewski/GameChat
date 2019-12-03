import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';
import { UsersService } from '../services/users.service';
import { FourInALineComponent } from '../four-in-a-line/four-in-a-line.component';
import { setTimeout } from 'timers';

@Component({
    selector: 'app-gaming-centre',
    templateUrl: './gaming-centre.component.html',
    styleUrls: ['./gaming-centre.component.css']
})
export class GamingCentreComponent implements OnInit {

    public keyword = "username";
    public users: User[] = []
    public selectedUser: User
    public currentUser: User
    public userReady: boolean = false;

    public get isUserInvalid() {
        return typeof this.selectedUser == 'string'
    }

    constructor(private usersService: UsersService) {
        usersService.getCurrentUser().subscribe((data: User) => this.currentUser = data)
    }

    ngOnInit() {
    }

    fetchUsers($event) {
        this.usersService.getUsers($event).
            subscribe((data: User[]) => {
                this.users = data.filter(u => u.id != this.currentUser.id)
            })
    }


    submitGameChallenge() {
        this.userReady = true
        console.log(this.selectedUser);
    }
}
