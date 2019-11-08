import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConversationsCentreComponent } from './conversations-centre.component';

describe('ConversationsCentreComponent', () => {
    let component: ConversationsCentreComponent;
    let fixture: ComponentFixture<ConversationsCentreComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ConversationsCentreComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ConversationsCentreComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
