import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class ConversationsService {

    constructor(private http: HttpClient) { }

    createConversation(conversation) {
        return this.http.post('/api/conversations', conversation)
    }

    getActiveConversations() {
        return this.http.get('/api/conversations')
    }
}
