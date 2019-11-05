import { Component, OnInit } from '@angular/core';
import { NotificationsService } from '../services/notifications.service';
import { Router } from '@angular/router';
import { Notifications, MessageNotification } from '../models/notifications';
import { MessagesService } from '../services/messages.service';

@Component({
    selector: 'app-notifications',
    templateUrl: './notifications.component.html',
    styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {

    private notifications: Notifications

    constructor(
        private notificationsService: NotificationsService,
        private router: Router)
    {
        this.notifications = new Notifications()
    }

    ngOnInit() {
        this.notificationsService.registerCallbackForReadingMessages(conversationid => {
            console.log('deleting notifications')

            let notifications = this.notifications.messageNotifications.find(n => n.conversationId = conversationid)

            if (notifications)
                this.notifications.messageNotifications = this.notifications.messageNotifications.filter(element => element.conversationId != conversationid)
        })

        this.notificationsService.startConnection()

        this.notificationsService.initialLoadMessageNotifications(
            (response: MessageNotification[]) => this.notifications.messageNotifications = response)

        this.notificationsService.receiveMessageNotification(conversationId => {
            if (!this.router.url.includes('/conversations/' + conversationId)) {
                this.addNotification(conversationId)
            }
        })
    }

    get unreadConversations() {
        return this.notifications.messageNotifications.length
    }

    clearNotifications() {
        this.notifications.messageNotifications.length = 0
    }

    private addNotification(response: number) {
        let notification = this.notifications.messageNotifications.find(n => n.conversationId == response)

        if (notification)
            notification.quantityOfUnreadMessages++
        else
            this.notifications.messageNotifications.push(new MessageNotification(response, 1))
    }
}
