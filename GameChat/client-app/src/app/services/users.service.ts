import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class UsersService {

    constructor(private http: HttpClient) { }

    getUsers() {
        return this.http.get('/api/account/all')
    }

    getCurrentUser() {
        return this.http.get('/api/account/current')
    }
}
