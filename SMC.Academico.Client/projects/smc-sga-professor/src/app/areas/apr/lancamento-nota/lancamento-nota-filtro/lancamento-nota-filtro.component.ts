import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { LancamentoNotaAvaliacaoModel } from '../models/lancamento-nota-avaliacao.model';
import { LancamentoNotaMapService } from '../services/lancamento-nota-map.service';

@Component({
  selector: 'app-lancamento-nota-filtro',
  templateUrl: './lancamento-nota-filtro.component.html'
})

export class LancamentoNotaFiltroComponent implements OnInit {
  @Input() avaliacoes: LancamentoNotaAvaliacaoModel[];
  avaliacoesOptions: any[];
  seqsAplicacaoAvaliacao: string[];
  ra = '';
  nome = '';
  @Output() filtrarAvaliacao = new EventEmitter<string[]>();
  @Output() filtrarAluno = new EventEmitter<{ ra: string, nome: string }>();

  constructor(private lancamentoNotaMapService: LancamentoNotaMapService) { }

  ngOnInit() {
    this.preencherModelo();
  }

  private preencherModelo() {
    this.avaliacoesOptions = this.lancamentoNotaMapService.mapearAvaliacoesOptions(this.avaliacoes);
    this.seqsAplicacaoAvaliacao = this.avaliacoes.map(m => m.seqAplicacaoAvaliacao);
  }

  aplicarFiltro() {
    this.avaliacaoChange();
    this.alunoChange();
  }

  avaliacaoChange() {
    this.filtrarAvaliacao.emit(this.seqsAplicacaoAvaliacao);
  }

  alunoChange() {
    this.filtrarAluno.emit({ ra: this.ra, nome: this.nome });
  }
}
