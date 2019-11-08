import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@aspnet/signalr'

@Injectable({
    providedIn: 'root'
})
export class ConversationsService {
    private hubConnection: signalR.HubConnection

    constructor(private http: HttpClient) { }

    startConnection() {
        this.hubConnection =
            new signalR.HubConnectionBuilder().
            withUrl('http://localhost:5000/hub/conversationfeed', { accessTokenFactory: () => localStorage.getItem('currentUser') }).
            build()

        this.hubConnection.start().then(() => this.hubConnection.invoke('GetConversationsForUser'))
    }

    stopConnection() {
        this.hubConnection.stop()
    }

    receiveConversations(callback) {
        this.hubConnection.on('InitialLoadOfConversations', data => callback(data))
    }

    updateConversationsFeed(callback) {
        this.hubConnection.on('UpdateFeedNewMessage', data => callback(data))
    }

    createConversation(conversation) {
        return this.http.post('/api/conversations', conversation)
    }

    getConversationInfo(conversationId: number) {
        return this.http.get('/api/conversations/' + conversationId)
    }
}
