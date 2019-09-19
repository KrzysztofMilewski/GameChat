import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';

import { User } from '../models/user';
import { ConversationsService } from '../services/conversations.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-conversation-start-form',
    templateUrl: './conversation-start-form.component.html',
    styleUrls: ['./conversation-start-form.component.css']
})
export class ConversationStartFormComponent implements OnInit {
    private enteredText: string
    public usersFromDatabase: User[]

    //TODO add conversation model (class) later on
    public conversation = {
        title: '',
        participants: []
    }

    constructor(
        private usersService: UsersService,
        private conversationsService: ConversationsService,
        private router: Router) { }

    ngOnInit() {
    }

    textChanged($event: string) {
       
        if ($event == '')
            return

        if (!$event.includes(this.enteredText)) {
            this.usersService.getUsers($event).
                subscribe((response: any) => this.usersFromDatabase = response.data)

            this.enteredText = $event
        }
    }

    createConversation() {
        console.log(this.conversation);

        this.conversationsService.createConversation(this.conversation).
            subscribe((result: any) => this.router.navigate(['/conversations/' + result.conversationId]))
    }
}
