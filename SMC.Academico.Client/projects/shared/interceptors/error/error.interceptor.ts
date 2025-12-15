import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, EMPTY, from } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { tryParseJSON } from 'projects/shared/utils/util';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private notificationService: SmcNotificationService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';
        const response = tryParseJSON(error.error);
        if (response.error && response.error.message && response.error.errorStack) {
          // client-side error
          errorMessage = response.error.message;
        } else if (error.error && error.error.message) {
          // client-side error
          errorMessage = error.error.message;
        } else if (error.error && error.error.error && error.error.error.message) {
          // client-side error
          errorMessage = error.error.error.message;
        } else {
          // server-side error
          errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        console.error(error);
        this.notificationService.error(errorMessage);
        return throwError(error);
      })
    ));
  }
}
