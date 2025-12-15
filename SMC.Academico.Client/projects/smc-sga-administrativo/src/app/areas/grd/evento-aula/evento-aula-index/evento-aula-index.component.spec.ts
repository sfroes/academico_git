import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaIndexComponent } from './evento-aula-index.component';

describe('EventoAulaIndexComponent', () => {
  let component: EventoAulaIndexComponent;
  let fixture: ComponentFixture<EventoAulaIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaIndexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
