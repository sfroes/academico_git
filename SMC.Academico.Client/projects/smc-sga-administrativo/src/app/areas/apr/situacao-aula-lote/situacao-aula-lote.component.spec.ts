import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoAulaLoteComponent } from './situacao-aula-lote.component';

describe('SituacaoAulaLoteComponent', () => {
  let component: SituacaoAulaLoteComponent;
  let fixture: ComponentFixture<SituacaoAulaLoteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoAulaLoteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoAulaLoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
