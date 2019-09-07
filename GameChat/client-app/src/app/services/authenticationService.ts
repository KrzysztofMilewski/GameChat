import { HttpClient } from '@angular/common/http';

export class AuthenticationService {
    constructor(private http: HttpClient) { }

    login(username: string, password: string) {
        return this.http.post('/api/account/authenticate', { username, password }).
            subscribe(token => {
                if (token)
                    localStorage.setItem('currentUser', JSON.stringify(token))
            })
    }

    logout() {
        localStorage.removeItem('currentUser')
    }
}
