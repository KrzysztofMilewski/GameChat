import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@aspnet/signalr'

@Injectable({
    providedIn: 'root'
})
export class MessagesService {

    private hubConnection: signalR.HubConnection

    constructor(private http: HttpClient) { }

    startConnection(conversationId: number) {
        this.hubConnection =
            new signalR.HubConnectionBuilder().
                withUrl('http://localhost:5000/hub/messages', { accessTokenFactory: () => localStorage.getItem('currentUser') }).
                build()

        this.hubConnection.start().
            then(() => {
                this.hubConnection.invoke('JoinConversation', conversationId).then(() => console.log('Joined conversation ' + conversationId))
            }).catch(error => console.log('ERROR: ' + error))
    }

    stopConnection() {
        this.hubConnection.stop()
    }

    addMessagesListener(callback) {
        this.hubConnection.on('SendMessage', data => callback(data))
    }

    loadMessages(conversationId: number) {
        return this.http.get('api/messages/' + conversationId)
    }

    sendMessage(message) {
        this.hubConnection.invoke('ReceiveMessageFromClient', message)
    }

    markMessageAsRead(messageId: number) {
        return this.http.delete('/api/messages/' + messageId)
    }
}
