import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmcLookupBuscaSimplesComponent } from './smc-lookup-busca-simples.component';

describe('SmcLookupBuscaSimplesComponent', () => {
  let component: SmcLookupBuscaSimplesComponent;
  let fixture: ComponentFixture<SmcLookupBuscaSimplesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmcLookupBuscaSimplesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmcLookupBuscaSimplesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
