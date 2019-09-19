import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConversationStartFormComponent } from './conversation-start-form.component';

describe('ConversationStartFormComponent', () => {
    let component: ConversationStartFormComponent;
    let fixture: ComponentFixture<ConversationStartFormComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ConversationStartFormComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ConversationStartFormComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
