import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-user-panel',
    templateUrl: './user-panel.component.html',
    styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {

    expanded: boolean = false

    constructor() { }

    ngOnInit() {
       
    }

    expand() {
        this.expanded = !this.expanded
    }
}
