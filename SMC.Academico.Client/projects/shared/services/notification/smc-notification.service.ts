import { Injectable } from '@angular/core';
import { PoNotificationService, PoNotification, PoToasterOrientation } from '@po-ui/ng-components';

@Injectable({
  providedIn: "root",
})
export class SmcNotificationService {
  constructor(private poNotificationService: PoNotificationService) {}

  success(message: string, duration: number = 30 * 1000) {
    const notification = this.createNotification(message, duration);
    this.poNotificationService.success(notification);
  }

  information(message: string, duration: number = 10 * 1000) {
    const notification = this.createNotification(message, duration);
    this.poNotificationService.information(notification);
  }

  warning(message: string, duration: number = 10 * 1000) {
    const notification = this.createNotification(message, duration);
    this.poNotificationService.warning(notification);
  }

  error(message: string, duration: number = 30 * 1000) {
    const notification = this.createNotification(message, duration);
    this.poNotificationService.error(notification);
  }

  private createNotification(
    message: string,
    duration: number
  ): PoNotification {
    return {
      message: message,
      orientation: PoToasterOrientation.Top,
      duration: duration,
    };
  }
}
