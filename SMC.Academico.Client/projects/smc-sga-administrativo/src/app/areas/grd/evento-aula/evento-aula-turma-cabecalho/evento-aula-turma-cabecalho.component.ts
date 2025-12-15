import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { EventoAulaTurmaCabecalhoModel } from '../../moldels/evento-aula-turma-cabecalho.model';
import { EventoAulaService } from '../../services/evento-aula.service';

@Component({
  selector: 'sga-evento-aula-turma-cabecalho',
  templateUrl: './evento-aula-turma-cabecalho.component.html',
})
export class EventoAulaTurmaCabecalhoComponent implements OnInit {
  @Input('e-dadosCabecalho') dadosCabecalho: EventoAulaTurmaCabecalhoModel;

  get dataSourceCarregando$() {
    return this.service.loadingDataSource;
  }

  constructor(private router: Router,
    private service: EventoAulaService) {}

  ngOnInit(): void {
  }

  abrirModalAssociarProfessor() {
    this.router.navigate([{ outlets: { modais: ['AssociarProfessor'] } }], {
      queryParamsHandling: 'preserve',
    });
  }
}
