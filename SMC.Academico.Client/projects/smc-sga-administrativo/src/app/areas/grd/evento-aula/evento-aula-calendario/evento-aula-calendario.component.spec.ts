import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaCalendarioComponent } from './evento-aula-calendario.component';

describe('EventoAulaCalendarioComponent', () => {
  let component: EventoAulaCalendarioComponent;
  let fixture: ComponentFixture<EventoAulaCalendarioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaCalendarioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaCalendarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
