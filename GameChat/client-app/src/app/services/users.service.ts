import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class UsersService {

    private endpoint: string = '/api/account'

    constructor(private http: HttpClient) { }

    getUsers(filter: string) {
        return this.http.get(this.endpoint + '/all?username=' + filter)
    }

    getCurrentUser() {
        return this.http.get(this.endpoint + '/current')
    }
}
