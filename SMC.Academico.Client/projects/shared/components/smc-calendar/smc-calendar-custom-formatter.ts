import { CalendarNativeDateFormatter, DateFormatterParams } from 'angular-calendar';
import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';

@Injectable()
export class SmcCalendarCustomFormatter extends CalendarNativeDateFormatter {

  constructor() {
    super();
  }

  // you can override any of the methods defined in the parent class
  public dayViewHour({ date, locale }: DateFormatterParams): string {
    return new DatePipe(locale).transform(date, 'HH:mm', locale);
  }

  public weekViewHour({ date, locale }: DateFormatterParams): string {
    return this.dayViewHour({ date, locale });
  }
}
