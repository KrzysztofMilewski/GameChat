import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';

import { User } from '../models/user';
import { ConversationsService } from '../services/conversations.service';
import { Router } from '@angular/router';
import { Conversation } from '../models/conversation';

@Component({
    selector: 'app-conversation-start-form',
    templateUrl: './conversation-start-form.component.html',
    styleUrls: ['./conversation-start-form.component.css']
})
export class ConversationStartFormComponent implements OnInit {
    usersFromDatabase: User[]
    currentUser: User
    conversation: Conversation

    constructor(
        private usersService: UsersService,
        private conversationsService: ConversationsService,
        private router: Router) {

        this.conversation = new Conversation()
        this.conversation.participants = []

        usersService.getCurrentUser().
            subscribe((data: User) => this.currentUser = data)
    }

    ngOnInit() {
        
    }

    textChanged($event: string) {
        if ($event == '')
            return

        this.usersService.getUsers($event).
            subscribe((response: any) => {
                this.usersFromDatabase = response.data
                this.usersFromDatabase = this.usersFromDatabase.filter(u => u.id != this.currentUser.id)
            })
    }

    createConversation() {
        if (this.conversation.participants.length == 0)
            return 

        this.conversationsService.createConversation(this.conversation).
            subscribe((result: any) => this.router.navigate(['/conversations/' + result.conversationId]))
    }
}
