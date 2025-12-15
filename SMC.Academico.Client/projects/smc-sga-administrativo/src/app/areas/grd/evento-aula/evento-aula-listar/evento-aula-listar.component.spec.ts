import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaListarComponent } from './evento-aula-listar.component';

describe('EventoAulaListarComponent', () => {
  let component: EventoAulaListarComponent;
  let fixture: ComponentFixture<EventoAulaListarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaListarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaListarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
