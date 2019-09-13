import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class MessagesService {

    constructor(private http: HttpClient) { }

    loadMessages(conversationId: number) {
        return this.http.get('api/messages/' + conversationId)
    }

    sendMessage(message) {
        return this.http.post('/api/messages', message)
    }
}
