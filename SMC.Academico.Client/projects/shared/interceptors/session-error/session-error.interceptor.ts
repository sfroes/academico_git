import { Injectable, Inject } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { DOCUMENT } from '@angular/common';
import { catchError } from 'rxjs/operators';

@Injectable()
export class SessionErrorInterceptor implements HttpInterceptor {
  constructor(@Inject(DOCUMENT) private document: Document) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err instanceof HttpErrorResponse) {
          const response = err as HttpErrorResponse;
          if (response.headers.has('SMC_SESSION_STATUS')) {
            this.document.location.href = '../Home/Logout';
          }
          return throwError(err);
        }
      })
    );
  }
}
