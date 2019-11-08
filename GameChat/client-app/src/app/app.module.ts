import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import bootstrap from "bootstrap";
import * as $ from 'jquery';
import { TagInputModule } from 'ngx-chips'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { LoginFormComponent } from './login-form/login-form.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './helpers/JwtInterceptor';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { HomeComponent } from './home/home.component';
import { UserPanelComponent } from './user-panel/user-panel.component';
import { RegisterFormComponent } from './register-form/register-form.component';
import { ConversationComponent } from './conversation/conversation.component';
import { ConversationStartFormComponent } from './conversation-start-form/conversation-start-form.component';
import { NotificationsComponent } from './notifications/notifications.component';
import { ConversationsCentreComponent } from './conversations-centre/conversations-centre.component';

@NgModule({
    declarations: [
        AppComponent,
        LoginFormComponent,
        NavigationBarComponent,
        HomeComponent,
        UserPanelComponent,
        RegisterFormComponent,
        ConversationComponent,
        ConversationStartFormComponent,
        NotificationsComponent,
        ConversationsCentreComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        ReactiveFormsModule,
        HttpClientModule,
        FormsModule,
        TagInputModule,
        BrowserAnimationsModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
