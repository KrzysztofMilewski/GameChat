import { Component, OnInit, Input } from '@angular/core';
import { User } from '../models/user';
import { FourInALineService } from '../services/four-in-a-line.service';
import { GameToken } from '../models/notifications';

@Component({
    selector: 'app-four-in-a-line',
    templateUrl: './four-in-a-line.component.html',
    styleUrls: ['./four-in-a-line.component.css']
})
export class FourInALineComponent implements OnInit {

    @Input() challengedUser: User
    @Input() gameToken: GameToken

    gameStarted: boolean

    constructor(private fourInALineService: FourInALineService) {

    }

    ngOnInit() {
        if (this.challengedUser)
            this.makeChallenge()
        else
            this.fourInALineService.startConnection()

        this.fourInALineService.challengeAccepted(data => {
            this.gameStarted = true
        })
    }

    private makeChallenge() {
        this.fourInALineService.startConnectionAndSendChallenge(this.challengedUser.id)
    }

    acceptChallenge() {
        this.fourInALineService.acceptChallenge(this.gameToken.gameId)
    }
}
