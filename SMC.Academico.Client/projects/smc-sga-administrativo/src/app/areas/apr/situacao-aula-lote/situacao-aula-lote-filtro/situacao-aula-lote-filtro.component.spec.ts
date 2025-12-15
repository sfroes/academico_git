import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoAulaLoteFiltroComponent } from './situacao-aula-lote-filtro.component';

describe('SituacaoAulaLoteFiltroComponent', () => {
  let component: SituacaoAulaLoteFiltroComponent;
  let fixture: ComponentFixture<SituacaoAulaLoteFiltroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoAulaLoteFiltroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoAulaLoteFiltroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
