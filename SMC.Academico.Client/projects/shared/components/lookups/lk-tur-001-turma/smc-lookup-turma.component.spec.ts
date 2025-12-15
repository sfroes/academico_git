import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcLookupTurmaComponent } from './smc-lookup-turma.component';

describe('SmcLookupTurmaComponent', () => {
  let component: SmcLookupTurmaComponent;
  let fixture: ComponentFixture<SmcLookupTurmaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcLookupTurmaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcLookupTurmaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
