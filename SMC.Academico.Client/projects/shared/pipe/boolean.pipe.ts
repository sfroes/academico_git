import { Pipe, PipeTransform } from '@angular/core';
import { CalendarWeekViewHourSegmentComponent } from 'angular-calendar/modules/week/calendar-week-view-hour-segment.component';

@Pipe({
  name: 'boolean',
})
export class BooleanPipe implements PipeTransform {
  /**
   * #### Description
   *  Boleanos em sim e não
   * Transforms boolean pipe
   * @param value
   * @returns transform
   */
  transform(value): string {
    switch (value) {
      case false:
        return 'Não';
      case true:
        return 'Sim';
      default:
        return '';
    }
  }
}
