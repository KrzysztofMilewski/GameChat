import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@aspnet/signalr'

@Injectable({
    providedIn: 'root'
})
export class MessagesService {

    private hubConnection: signalR.HubConnection

    constructor(private http: HttpClient) { }

    startConnection() {
        this.hubConnection =
            new signalR.HubConnectionBuilder().
                withUrl('http://localhost:5000/messages').
                build()

        //TODO Console logging is added for debugging/development purposes. Remove it afterwards
        this.hubConnection.start().then(() => console.log('connection started')).catch(error => console.log('ERROR: ' + error))
    }

    addMessagesListener(callback) {
        this.hubConnection.on('SendMessage', data => callback(data))
    }

    loadMessages(conversationId: number) {
        return this.http.get('api/messages/' + conversationId)
    }

    sendMessage(message) {
        return this.http.post('/api/messages', message)
    }
}
