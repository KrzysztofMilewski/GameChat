import { Component, OnInit } from '@angular/core';
import { MessagesService } from '../services/messages.service';
import { ActivatedRoute } from '@angular/router';
import { Message } from '../models/message';
import { UsersService } from '../services/users.service';

@Component({
    selector: 'app-conversation',
    templateUrl: './conversation.component.html',
    styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit {

    private conversationId: number
    private messageList: Message[]

    //TODO change to an object
    private messageToSend: any = {}
    //TODO add user class
    private currentUser: any = {}


    constructor(
        private messageService: MessagesService,
        private activatedRoute: ActivatedRoute,
        private usersService: UsersService) {

        activatedRoute.params.subscribe(p => {
            this.conversationId = +p['id']
            this.messageToSend.conversationId = this.conversationId
        })

        usersService.getCurrentUser().
            subscribe(data => this.currentUser = data)
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
        //TODO  replace with an object
        this.messageService.sendMessage(this.messageToSend).
            subscribe(m => {
                this.messageToSend.contents = ''
            })
    }

    isMessageMine(senderId: number) {
        return senderId == this.currentUser.id ? true : false
    }
}
