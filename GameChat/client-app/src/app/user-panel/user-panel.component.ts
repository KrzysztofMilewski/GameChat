import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { ConversationsService } from '../services/conversations.service';
import { User } from '../models/user';
import { Router } from '@angular/router';

@Component({
    selector: 'app-user-panel',
    templateUrl: './user-panel.component.html',
    styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {

    currentUser: User
    expanded: boolean = false
    activeConversations: any[]


    constructor(
        private userService: UsersService,
        private conversationsService: ConversationsService,
        private router: Router) { }

    //TODO maybe add classes for conversations
    ngOnInit() {
        this.conversationsService.getActiveConversations().
            subscribe((data: any[]) => this.activeConversations = data)

        this.userService.getCurrentUser().
            subscribe((user: User) => this.currentUser = user)
    }

    expand() {
        this.expanded = !this.expanded

        console.log(this.displayParticipants(this.activeConversations[0]))
    }

    isGroupChat(conversation): boolean {
        return conversation.participants.length > 2 ? true : false
    }

    displayParticipants(conversation): string {
        if (conversation.participants.length == 2)
            return conversation.participants.find(u => u.id != this.currentUser.id).username;
        else {
            let shortestName = this.findShortestName(conversation.participants)
            return shortestName + ' and ' + (conversation.participants.length - 2) + ' others'
        }
    }

    navigateToConversation(conversationdId: number) {
        this.router.navigate(['/conversations/' + conversationdId])
    }

    private findShortestName(participants: User[]): string {
        let shortest = participants[0].username

        for (let i = 1; i < participants.length; i++) {
            if (participants[i].username.length < shortest.length)
                shortest = participants[i].username
        }

        return shortest
    }
}
