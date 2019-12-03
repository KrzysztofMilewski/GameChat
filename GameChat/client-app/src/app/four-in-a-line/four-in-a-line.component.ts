import { Component, OnInit, Input } from '@angular/core';
import { User } from '../models/user';
import { FourInALineService } from '../services/four-in-a-line.service';

@Component({
    selector: 'app-four-in-a-line',
    templateUrl: './four-in-a-line.component.html',
    styleUrls: ['./four-in-a-line.component.css']
})
export class FourInALineComponent implements OnInit {

    @Input() challengedUser: User

    constructor(private fourInALineService: FourInALineService) {
        console.log("4 in a line - ctor");
    }

    ngOnInit() {
        this.fourInALineService.startConnectionAndSendChallenge(this.challengedUser.id)
    }
}
