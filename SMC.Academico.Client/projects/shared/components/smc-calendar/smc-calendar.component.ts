import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { PoSelectOption } from '@po-ui/ng-components';
import { CalendarDateFormatter, CalendarEvent, CalendarMonthViewBeforeRenderEvent, CalendarView, CalendarWeekViewBeforeRenderEvent } from 'angular-calendar';
import { SmcCalendarCustomFormatter } from './smc-calendar-custom-formatter';

@Component({
  selector: 'smc-calendar',
  templateUrl: './smc-calendar.component.html',
  providers: [
    {
      provide: CalendarDateFormatter,
      useClass: SmcCalendarCustomFormatter,
    },
  ],
})
export class SmcCalendarComponent implements OnInit {
  CalendarView = CalendarView;
  viewDate: Date = new Date();
  locale: string = 'pt-BR';
  viewMonthHighlight = false;
  viewWeekHighlight = false;

  @Input() events: CalendarEvent[];
  @Input('s-cell-template') cellTemplate: TemplateRef<any>;
  @Input('s-day-event-template') dayEventTemplate: TemplateRef<any>;
  @Input('s-week-event-template') weekEventTemplate: TemplateRef<any>;
  @Input('s-day-hour-segment-template') dayHourSegmentTemplate: TemplateRef<any>;
  @Input('s-week-hour-segment-template') weekHourSegmentTemplate: TemplateRef<any>;
  @Input('s-week-current-time-marker-template') weekCurrentTimeMarkerTemplate: TemplateRef<any>;
  @Input('s-default-view') view = CalendarView.Month;
  @Output('s-before-view-render-month') beforeViewRenderMonth = new EventEmitter<CalendarMonthViewBeforeRenderEvent>();
  @Output('s-before-view-render-week') beforeViewRenderWeek = new EventEmitter<CalendarWeekViewBeforeRenderEvent>();
  @Output('s-before-view-render-day') beforeViewRenderDay = new EventEmitter<CalendarWeekViewBeforeRenderEvent>();
  constructor() {}

  ngOnInit(): void {
    this.viewMonthHighlight = this.view === CalendarView.Month;
    this.viewWeekHighlight = this.view === CalendarView.Week;
  }

  views: PoSelectOption[] = [
    { label: 'Mês', value: CalendarView.Month },
    { label: 'Semana', value: CalendarView.Week },
  ];

  callView(view: CalendarView) {
    this.view = view;
    this.viewMonthHighlight = view === CalendarView.Month;
    this.viewWeekHighlight = view === CalendarView.Week;
  }
}
