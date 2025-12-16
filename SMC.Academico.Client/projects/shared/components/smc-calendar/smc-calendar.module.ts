import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcCalendarComponent } from './smc-calendar.component';
import {
  CalendarDateFormatter,
  CalendarModule,
  CalendarMomentDateFormatter,
  DateAdapter,
  MOMENT,
} from 'angular-calendar';
import { ContextMenuModule } from '@perfectmemory/ngx-contextmenu';
import { SmcButtonModule } from '../smc-button/smc-button.module';
import { PoUiModule } from 'projects/shared/modules/po-ui.module';
import { FormsModule } from '@angular/forms';
import * as moment from 'moment';
import { adapterFactory } from 'angular-calendar/date-adapters/moment';

export function momentAdapterFactory() {
  return adapterFactory(moment);
}

@NgModule({
  declarations: [SmcCalendarComponent],
  imports: [
    CommonModule,
    FormsModule,
    SmcButtonModule,
    PoUiModule,
    CalendarModule.forRoot(
      {
        provide: DateAdapter,
        useFactory: momentAdapterFactory,
      },
      {
        dateFormatter: {
          provide: CalendarDateFormatter,
          useClass: CalendarMomentDateFormatter,
        },
      }
    ),
  ],
  exports: [SmcCalendarComponent, ContextMenuModule],
  providers: [
    {
      provide: MOMENT,
      useValue: moment,
    },
  ],
})
export class SmcCalendarModule {}
