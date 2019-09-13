import { Component, OnInit } from '@angular/core';
import { MessagesService } from '../services/messages.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-conversation',
    templateUrl: './conversation.component.html',
    styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit {

    private conversationId: number
    private messageList: string[] = []
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
            subscribe((data: any[]) => {
                for (let message of data)
                    this.messageList.push(message.contents)
            })
    }

    onSubmit() {
        //TODO  replace with an object
        this.messageService.sendMessage(this.messageToSend).
            subscribe(m => {
                console.log("MESSAGES: ");
                console.log(this.messageList);
                this.messageList.push('' + this.messageToSend.contents)
                delete this.messageToSend.contents
            })
    }
}
