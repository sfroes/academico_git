import { Injectable, Inject } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, EMPTY, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { DOCUMENT } from '@angular/common';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
  constructor(@Inject(DOCUMENT) private document: Document) { }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(err => {
        if (err instanceof HttpErrorResponse) {
          const response = err as HttpErrorResponse;
          if (response.url.includes('SAS.Login')) {
            this.document.location.href = response.url;
          }
          return throwError(err);
        }
      })
    );
  }
}
