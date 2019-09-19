import { Component, OnInit } from '@angular/core';
import { UsersService } from '../services/users.service';
import { ConversationsService } from '../services/conversations.service';

@Component({
    selector: 'app-user-panel',
    templateUrl: './user-panel.component.html',
    styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {

    expanded: boolean = false

    constructor(
        private userService: UsersService,
        private conversationsService: ConversationsService) { }

    ngOnInit() {
    }

    expand() {
        this.expanded = !this.expanded
    }
}
