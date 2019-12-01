import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent } from './login-form/login-form.component';
import { HomeComponent } from './home/home.component';
import { UserPanelComponent } from './user-panel/user-panel.component';
import { AuthGuard } from './guards/auth';
import { RegisterFormComponent } from './register-form/register-form.component';
import { ConversationComponent } from './conversation/conversation.component';
import { ConversationsCentreComponent } from './conversations-centre/conversations-centre.component';
import { GamingCentreComponent } from './gaming-centre/gaming-centre.component';


const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginFormComponent },
    { path: 'register', component: RegisterFormComponent },
    { path: 'user', component: UserPanelComponent, canActivate: [AuthGuard] },
    { path: 'conversations/:id', component: ConversationComponent, canActivate: [AuthGuard] },
    { path: 'conversations', component: ConversationsCentreComponent, canActivate: [AuthGuard] },
    { path: 'gaming', component: GamingCentreComponent, canActivate: [AuthGuard] }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
