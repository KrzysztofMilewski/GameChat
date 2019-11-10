import { Component, OnInit, OnDestroy, AfterViewInit, AfterViewChecked } from '@angular/core';
import { MessagesService } from '../services/messages.service';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { Message } from '../models/message';
import { UsersService } from '../services/users.service';
import { User } from '../models/user';
import { ConversationsService } from '../services/conversations.service';
import { Conversation } from '../models/conversation';
import { NotificationsService } from '../services/notifications.service';

@Component({
    selector: 'app-conversation',
    templateUrl: './conversation.component.html',
    styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit, OnDestroy, AfterViewChecked {

    private conversation: Conversation = new Conversation()
    private currentUser: User
    private messageList: Message[]
    private messageToSend: Message = new Message()
    private firstLoad: boolean = true


    constructor(
        private messageService: MessagesService,
        private activatedRoute: ActivatedRoute,
        private usersService: UsersService,
        private conversationService: ConversationsService,
        private notificationsService: NotificationsService) {

        
        activatedRoute.paramMap.subscribe(params => {
            if (this.firstLoad)
                this.firstLoad = false
            else {
                this.ngOnDestroy()
                this.ngOnInit()
            }
        })
    }

    ngOnInit() {
        this.messageToSend.conversationId = this.conversation.id = +this.activatedRoute.snapshot.paramMap.get('id')

        this.usersService.getCurrentUser().
            subscribe(user => {
                this.currentUser = this.messageToSend.sender = user as User
            })

        this.conversationService.getConversationInfo(this.conversation.id).
            subscribe((data: Conversation) => this.conversation = data)

        this.messageService.loadMessages(this.conversation.id).
            subscribe((data: Message[]) => {
                this.messageList = data
                this.notificationsService.notifyAboutReadingMessages(this.conversation.id)
            })

        this.messageService.startConnection(this.conversation.id)
        this.messageService.addMessagesListener(receivedMessage => {
            this.messageList.push(Object.assign({}, receivedMessage))

            if (receivedMessage.sender.id != this.currentUser.id) {
                this.messageService.markMessageAsRead(receivedMessage.id).
                    subscribe(data => console.log(data))
            }
        })
    }

    ngOnDestroy() {
        this.messageService.stopConnection()
    }

    ngAfterViewChecked() {
        var messagesDiv = document.getElementById('scrollMe')
        messagesDiv.scrollTop = messagesDiv.scrollHeight
    }

    onSubmit() {
        this.messageService.sendMessage(this.messageToSend)
        this.messageToSend.contents = ''
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
