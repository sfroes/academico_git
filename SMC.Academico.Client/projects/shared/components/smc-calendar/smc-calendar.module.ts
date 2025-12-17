import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcCalendarComponent } from './smc-calendar.component';
import {
  CalendarModule,
  DateAdapter,
} from 'angular-calendar';
import { ContextMenuModule } from '@perfectmemory/ngx-contextmenu';
import { SmcButtonModule } from '../smc-button/smc-button.module';
import { PoUiModule } from 'projects/shared/modules/po-ui.module';
import { FormsModule } from '@angular/forms';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';

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
        useFactory: adapterFactory,
      }
    ),
  ],
  exports: [SmcCalendarComponent, ContextMenuModule],
})
export class SmcCalendarModule {}
