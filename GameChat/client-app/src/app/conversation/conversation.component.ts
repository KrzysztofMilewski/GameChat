import { Component, OnInit } from '@angular/core';
import { MessagesService } from '../services/messages.service';
import { ActivatedRoute } from '@angular/router';
import { Message } from '../models/message';
import { UsersService } from '../services/users.service';
import { forkJoin } from 'rxjs';
import { User } from '../models/user';

@Component({
    selector: 'app-conversation',
    templateUrl: './conversation.component.html',
    styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit {

    //TODO add fetching data about conversation (title, participants etc.)

    private currentUser: User
    private conversationId: number

    private messageList: Message[]
    private messageToSend: Message = new Message()

    constructor(
        private messageService: MessagesService,
        private activatedRoute: ActivatedRoute,
        private usersService: UsersService) {

        this.messageToSend.conversationId = this.conversationId = +activatedRoute.snapshot.paramMap.get('id')

        usersService.getCurrentUser().
            subscribe(user => {
                this.currentUser = this.messageToSend.sender = user as User
            })
    }

    ngOnInit() {
        this.messageService.loadMessages(this.conversationId).
            subscribe((data: Message[]) => {
                this.messageList = data
            })

        this.messageService.startConnection()
        this.messageService.addMessagesListener(receivedMessage => {
            this.messageList.push(Object.assign({}, receivedMessage))
        })
    }

    onSubmit() {
        this.messageService.sendMessage(this.messageToSend).
            subscribe(m => {
                this.messageToSend.contents = ''
            })
    }

    isMessageMine(senderId: number) {
        return senderId == this.currentUser.id ? true : false
    }
}
