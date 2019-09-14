import { Component, OnInit } from '@angular/core';
import { MessagesService } from '../services/messages.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Message } from '../models/message';

@Component({
    selector: 'app-conversation',
    templateUrl: './conversation.component.html',
    styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit {

    private conversationId: number
    private messageList: Message[]
    private messageToSend: any = {}


    constructor(
        private messageService: MessagesService,
        private activatedRoute: ActivatedRoute,
        private router: Router) {

        activatedRoute.params.subscribe(p => {
            this.conversationId = +p['id']
            this.messageToSend.conversationId = this.conversationId
        })
    }

    ngOnInit() {
        this.messageService.loadMessages(this.conversationId).
            subscribe((data: Message[]) => {
                this.messageList = data
            })
    }

    onSubmit() {
        //TODO  replace with an object
        this.messageService.sendMessage(this.messageToSend).
            subscribe(m => {
                this.messageList.push(Object.assign({}, this.messageToSend))
                delete this.messageToSend.contents
            })
    }
}
