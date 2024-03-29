import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    constructor(private http: HttpClient) { }

    login(username: string, password: string) {
        return this.http.post('/api/account/authenticate', { username, password })
    }

    register(username: string, password: string) {
        return this.http.post('/api/account/create', { username, password })
    }

    logout() {
        localStorage.removeItem('currentUser')
    }

    isLoggedIn() {
        if (localStorage.getItem('currentUser'))
            return true
        else
            return false
    }
}
