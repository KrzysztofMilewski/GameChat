import { Component, OnInit } from '@angular/core';
import { MessagesService } from '../services/messages.service';
import { ActivatedRoute } from '@angular/router';
import { Message } from '../models/message';
import { UsersService } from '../services/users.service';
import { forkJoin } from 'rxjs';
import { User } from '../models/user';
import { ConversationsService } from '../services/conversations.service';
import { Conversation } from '../models/conversation';

@Component({
    selector: 'app-conversation',
    templateUrl: './conversation.component.html',
    styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit {

    //TODO add fetching data about conversation (title, participants etc.)

    private conversation: Conversation = new Conversation()
    private currentUser: User
    private messageList: Message[]
    private messageToSend: Message = new Message()

    constructor(
        private messageService: MessagesService,
        private activatedRoute: ActivatedRoute,
        private usersService: UsersService,
        private conversationService: ConversationsService) {

        this.messageToSend.conversationId = this.conversation.id = +activatedRoute.snapshot.paramMap.get('id')

        usersService.getCurrentUser().
            subscribe(user => {
                this.currentUser = this.messageToSend.sender = user as User
            })

        conversationService.getConversationInfo(this.conversation.id).
            subscribe((data: Conversation) => this.conversation = data)
    }

    ngOnInit() {
        this.messageService.loadMessages(this.conversation.id).
            subscribe((data: Message[]) => {
                this.messageList = data
            })

        this.messageService.startConnection()
        this.messageService.addMessagesListener(receivedMessage => {
            this.messageList.push(Object.assign({}, receivedMessage))

            if (receivedMessage.sender.id != this.currentUser.id) {
                this.messageService.markMessageAsRead(receivedMessage.id).
                    subscribe(data => console.log(data))
            }
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

    newMessagesBulk(messageIndex: number) {
        if (messageIndex == 0 || this.messageList[messageIndex].sender.id != this.messageList[messageIndex - 1].sender.id)
            return true
        else
            return false
    }
}
