import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        //TODO Add automatic logout on 401 response
        let currentUser = localStorage.getItem('currentUser')
        if (currentUser) {
            request = request.clone({
                setHeaders: {
                    Authorization: 'Bearer ' + currentUser
                }
            })
        }

        return next.handle(request);
    }
}
