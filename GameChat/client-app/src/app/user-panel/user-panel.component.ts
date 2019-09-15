import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { ConversationsService } from '../services/conversations.service';

@Component({
    selector: 'app-user-panel',
    templateUrl: './user-panel.component.html',
    styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {

    expanded: boolean = false
    hasBeenExpanded: boolean = false
    users: any[]

    constructor(
        private userService: UsersService,
        private conversationsService: ConversationsService) { }

    ngOnInit() {
    }


    // TODO change it to a list with tag input field
    expand() {
        this.expanded = !this.expanded

        if (!this.hasBeenExpanded)
            this.loadUsers()

        this.hasBeenExpanded = true
    }

    loadUsers() {
        this.userService.getUsers().
            subscribe((data: any[]) => {
                this.users = data
            })
    }


    //TODO Add redirection later on
    createConversation(userId) {
        console.log(userId)
        let conversation = {
            title: "Sample conversation",
            participants: [
                { id: userId }
            ]
        }

        this.conversationsService.createConversation(conversation).
            subscribe(result => console.log(result))
    }
}
