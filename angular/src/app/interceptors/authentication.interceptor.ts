import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const auth = localStorage.getItem('userData');
    if (auth) {
      const user = JSON.parse(auth);
      const token = user['access_token'];
      const req = request.clone({
        headers: request.headers.append('Authorization', `Bearer ${token}`),
      });
      request = req;
    }
    return next.handle(request);
  }
}
