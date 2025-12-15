import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoAulaBiTreeLocalComponent } from './evento-aula-bi-tree-local.component';

describe('EventoAulaBiTreeLocalComponent', () => {
  let component: EventoAulaBiTreeLocalComponent;
  let fixture: ComponentFixture<EventoAulaBiTreeLocalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventoAulaBiTreeLocalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventoAulaBiTreeLocalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
