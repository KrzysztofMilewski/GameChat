import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr'

@Injectable({
    providedIn: 'root'
})
export class NotificationsService {

    private hubConnection: signalR.HubConnection
    private readMessagesCallback

    startConnection() {
        this.hubConnection = new signalR.HubConnectionBuilder().
            withUrl('http://localhost:5000/hub/notifications', { accessTokenFactory: () => localStorage.getItem('currentUser') }).
            build()

        this.hubConnection.start().
            then(() => {
                this.hubConnection.invoke('GetUnreadMessagesNotifications')
            })
    }

    receiveMessageNotification(callback) {
        this.hubConnection.on('MessageNotification', data => callback(data))
    }

    initialLoadMessageNotifications(callback) {
        this.hubConnection.on('InitialNotificationsLoad', data => callback(data))
    }

    notifyAboutReadingMessages(conversationId: number) {
        this.readMessagesCallback(conversationId)
    }

    registerCallbackForReadingMessages(callback) {
        this.readMessagesCallback = callback
    }
}
