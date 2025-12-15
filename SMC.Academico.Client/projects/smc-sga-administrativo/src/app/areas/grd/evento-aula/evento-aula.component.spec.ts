import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaComponent } from './evento-aula.component';

describe('EventoAulaComponent', () => {
  let component: EventoAulaComponent;
  let fixture: ComponentFixture<EventoAulaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
