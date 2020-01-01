import { Component, OnInit, Input } from '@angular/core';
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

    @Input() challengedUser: User
    @Input() gameToken: GameToken

    public board: FourInALineField[]

    gameStarted: boolean
    gameId: string

    constructor(private fourInALineService: FourInALineService) {

    }

    ngOnInit() {
        if (this.challengedUser)
            this.makeChallenge()
        else
            this.fourInALineService.startConnection()

        this.fourInALineService.challengeAccepted((gameId, board) => {
            this.gameStarted = true
            this.gameId = gameId
            this.board = board

            console.log(this.board);
        })
    }

    acceptChallenge() {
        this.fourInALineService.acceptChallenge(this.gameToken.gameId)
    }

    private makeChallenge() {
        this.fourInALineService.startConnectionAndSendChallenge(this.challengedUser.id)
    }
}
