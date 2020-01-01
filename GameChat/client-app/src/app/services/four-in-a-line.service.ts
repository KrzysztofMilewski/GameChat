import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr'

@Injectable({
    providedIn: 'root'
})
export class FourInALineService {

    private hubConnection: signalR.HubConnection

    constructor() { }

    startConnectionAndSendChallenge(playerId: number) {
        this.hubConnection = new signalR.HubConnectionBuilder().
            withUrl('http://localhost:5000/hub/fourinaline', { accessTokenFactory: () => localStorage.getItem('currentUser') }).
            build()

        this.hubConnection.start().then(() => {
            this.hubConnection.invoke('ChallengePlayer', playerId)
        })
    }

    startConnection() {
        this.hubConnection = new signalR.HubConnectionBuilder().
            withUrl('http://localhost:5000/hub/fourinaline', { accessTokenFactory: () => localStorage.getItem('currentUser') }).
            build()

        this.hubConnection.start()
    }

    acceptChallenge(gameId: string) {
        this.hubConnection.invoke('AcceptChallenge', gameId)
    }

    challengeAccepted(callback) {
        this.hubConnection.on('Accepted', (gameId, board)  => callback(gameId, board))
    }
}
