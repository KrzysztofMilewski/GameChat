import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from '../models/user';
import { FourInALineService } from '../services/four-in-a-line.service';
import { GameToken } from '../models/notifications';
import { FourInALineField } from '../models/fourInALineField';

@Component({
    selector: 'app-four-in-a-line',
    templateUrl: './four-in-a-line.component.html',
    styleUrls: ['./four-in-a-line.component.css']
})
export class FourInALineComponent implements OnInit {

    @Input() currentUser: User
    @Input() challengedUser: User
    @Input() gameToken: GameToken
    @Output() finishedGameEvent = new EventEmitter()

    public board: FourInALineField[]

    public gameStarted: boolean
    public gameId: string
    public gameFinished: boolean = false


    private winnerId: number

    constructor(private fourInALineService: FourInALineService) {

    }

    isFieldMine(playerField: number) {
        if (playerField == 1) {
            if (this.player1 == this.currentUser)
                return true;
            else
                return false;
        }
        else {
            if (this.player2 == this.currentUser)
                return true;
            else
                return false;
        }
    }

    get player1() {
        if (this.challengedUser)
            return this.currentUser
        else
            return this.gameToken.challengingUser
    }

    get player2() {
        if (this.gameToken)
            return this.currentUser
        else
            return this.challengedUser
    }

    get opponent() {
        if (this.challengedUser)
            return this.challengedUser
        else
            return this.gameToken.challengingUser
    }

    get winner() {
        if (this.winnerId == this.player1.id)
            return this.player1
        else
            return this.player2
    }

    ngOnInit() {
        if (this.challengedUser)
            this.makeChallenge()
        else
            this.fourInALineService.startConnection()

        this.fourInALineService.challengeAccepted((gameId, board: FourInALineField[]) => {
            this.gameStarted = true
            this.gameId = gameId
            this.board = board
        })

        this.fourInALineService.discPlacedByEnemy((data: FourInALineField[]) => {
            this.board = data
        })

        this.fourInALineService.announceWinner((data: number) => {
            this.winnerId = data
            this.gameFinished = true;

            (<any>$('#winner-modal')).modal()
            
        })
    }

    makeMove(x: number) {
        this.fourInALineService.makeMove(this.gameId, x)
    }

    acceptChallenge() {
        this.fourInALineService.acceptChallenge(this.gameToken.gameId)
    }

    redirectToMainPage() {
        this.finishedGameEvent.emit()
    }

    private makeChallenge() {
        this.fourInALineService.startConnectionAndSendChallenge(this.challengedUser.id)
    }
}
