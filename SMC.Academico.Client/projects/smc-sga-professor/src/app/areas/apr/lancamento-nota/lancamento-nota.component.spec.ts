import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LancamentoNotaComponent } from './lancamento-nota.component';

describe('LancamentoNotaComponent', () => {
  let component: LancamentoNotaComponent;
  let fixture: ComponentFixture<LancamentoNotaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LancamentoNotaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LancamentoNotaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
