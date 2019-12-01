import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-four-in-a-line',
    templateUrl: './four-in-a-line.component.html',
    styleUrls: ['./four-in-a-line.component.css']
})
export class FourInALineComponent implements OnInit {

    constructor() {
        console.log("4 in a line - ctor");
    }

    ngOnInit() {
        console.log("4 in a line - init");
    }
}
