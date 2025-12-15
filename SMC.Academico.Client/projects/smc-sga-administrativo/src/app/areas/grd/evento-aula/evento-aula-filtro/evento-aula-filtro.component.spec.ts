import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaFiltroComponent } from './evento-aula-filtro.component';

describe('EventoAulaFiltroComponent', () => {
  let component: EventoAulaFiltroComponent;
  let fixture: ComponentFixture<EventoAulaFiltroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaFiltroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaFiltroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
