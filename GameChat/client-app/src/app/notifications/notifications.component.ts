import { Component, OnInit } from '@angular/core';
import { NotificationsService } from '../services/notifications.service';
import { Router } from '@angular/router';
import { Notifications, MessageNotification, GameToken } from '../models/notifications';
import { forkJoin } from 'rxjs';
import { ConversationsService } from '../services/conversations.service';
import { Conversation } from '../models/conversation';
import { UsersService } from '../services/users.service';
import { User } from '../models/user';

@Component({
    selector: 'app-notifications',
    templateUrl: './notifications.component.html',
    styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {

    private notifications: Notifications
    private conversations: Conversation[]
    private dropdownUpToDate: boolean

    constructor(
        private notificationsService: NotificationsService,
        private router: Router,
        private conversationsService: ConversationsService,
        private userService: UsersService) {
        this.notifications = new Notifications()
        this.conversations = []
        this.dropdownUpToDate = false
    }

    ngOnInit() {
        this.notificationsService.registerCallbackForReadingMessages(conversationid => {
            let notifications = this.notifications.messageNotifications.find(n => n.conversationId == conversationid)

            if (notifications) {
                this.notifications.messageNotifications = this.notifications.messageNotifications.filter(element => element.conversationId != conversationid)
                this.conversations = this.conversations.filter(element => element.id != conversationid)
            }
        })

        this.notificationsService.startConnection()

        this.notificationsService.initialLoadMessageNotifications(
            (response: MessageNotification[]) => this.notifications.messageNotifications = response)

        this.notificationsService.receiveMessageNotification(conversationId => {
            if (!this.router.url.includes('/conversations/' + conversationId))
                this.addNotification(conversationId)
        })

        this.notificationsService.receiveGameChallenge(gameChallengeToken => {

            this.userService.getUserById(gameChallengeToken.invitingPlayerId).
                subscribe((user: User) => {
                    let notification = new GameToken(
                        gameChallengeToken.gameId,
                        gameChallengeToken.gameName,
                        user,
                        gameChallengeToken.expirationTime)

                    this.notifications.gameNotifications.push(notification)
                })          
        })
    }

    get unreadConversations() {
        return this.notifications.messageNotifications.length
    }

    get gameInvites() {
        return this.notifications.gameNotifications.length
    }

    getConversationsInfo() {
        if (this.dropdownUpToDate)
            return

        let conversationIds = []
        for (let i = 0; i < this.notifications.messageNotifications.length; i++)
            conversationIds.push(this.notifications.messageNotifications[i].conversationId)

        let apiCallsForConversations = []
        for (let convId of conversationIds)
            apiCallsForConversations.push(this.conversationsService.getConversationInfo(+convId))

        forkJoin(apiCallsForConversations).
            subscribe((data: Conversation[]) => {
                for (let conversationInfo of data) {
                    conversationInfo.unreadMessages = this.notifications.messageNotifications.
                        find(n => n.conversationId == conversationInfo.id).quantityOfUnreadMessages

                    this.conversations.push(conversationInfo)
                }

                this.dropdownUpToDate = true
            })
    }

    clearNotifications() {
        this.notifications.messageNotifications.length = 0
    }

    navigateToGaming(gameId: string) {
        let token = this.notifications.gameNotifications.find(g => g.gameId == gameId)
        this.notifications.gameNotifications = this.notifications.gameNotifications.filter(g => g.gameId != gameId)


        this.router.navigate(['/gaming'], { state: { gameToken: token } })
    }

    private addNotification(response: number) {
        this.dropdownUpToDate = false
        this.conversations = []

        let notification = this.notifications.messageNotifications.find(n => n.conversationId == response)

        if (notification)
            notification.quantityOfUnreadMessages++
        else
            this.notifications.messageNotifications.push(new MessageNotification(response, 1))
    }
}
