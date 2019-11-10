import { Component, OnInit, OnDestroy } from '@angular/core';
import { User } from '../models/user';
import { UsersService } from '../services/users.service';
import { ConversationsService } from '../services/conversations.service';
import { Conversation } from '../models/conversation';
import { Router } from '@angular/router';

@Component({
    selector: 'app-conversations-centre',
    templateUrl: './conversations-centre.component.html',
    styleUrls: ['./conversations-centre.component.css']
})
export class ConversationsCentreComponent implements OnInit, OnDestroy {

    private currentUser: User
    newConversationFormExpanded: boolean = false

    conversations: Conversation[]

    constructor(
        private usersService: UsersService,
        private conversationsService: ConversationsService,
        private router: Router) {

        usersService.getCurrentUser().
            subscribe((data: User) => this.currentUser = data)

        this.conversations = []
    }

    ngOnInit() {
        this.conversationsService.startConnection()

        this.conversationsService.receiveConversations((data: any[]) => {
            data.forEach((conv) => {
                this.conversations.push({
                    id: conv.conversation.id,
                    title: conv.conversation.title,
                    participants: conv.conversation.participants,
                    unreadMessages: conv.unreadMessages,
                    dateOfLastMessage: conv.lastMessageSent
                })
            })
        })

        this.conversationsService.updateConversationsFeed(data => {
            let conversation = this.conversations.find(c => c.id == data.conversationId)
            conversation.dateOfLastMessage = data.lastMessageSent
            conversation.unreadMessages += 1

            this.conversations.sort((c1, c2) => +(c1.dateOfLastMessage < c2.dateOfLastMessage))
        })
    }

    ngOnDestroy() {
        this.conversationsService.stopConnection()
    }

    isGroupChat(conversation): boolean {
        return conversation.participants.length > 2 ? true : false
    }

    navigateToConversation(conversationdId: number) {
        this.router.navigate(['/conversations/' + conversationdId])
    }

    displayParticipants(conversation: Conversation): string {
        if (conversation.participants.length == 2)
            return conversation.participants.find(u => u.id != this.currentUser.id).username;
        else {
            let shortestName = this.findShortestName(conversation.participants.filter(p => p.id != this.currentUser.id))
            return shortestName + ' and ' + (conversation.participants.length - 2) + ' others'
        }
    }

    expand() {
        this.newConversationFormExpanded = !this.newConversationFormExpanded
    }

    private findShortestName(participants: User[]): string {
        let shortest = participants[0].username

        for (let i = 1; i < participants.length; i++) {
            if (participants[i].username.length < shortest.length)
                shortest = participants[i].username
        }

        return shortest
    }

    private compareDatesInConversations() {
        return (c1, c2) => {
            if (c1.dateOfLastMessage == null && c2.dateOfLastMessage != null)
                return -1
            if (c2.dateOfLastMessage == null && c1.dateOfLastMessage != null)
                return 1
            else
                return +(c1.dateOfLastMessage < c2.dateOfLastMessage)
        }
    }
}
