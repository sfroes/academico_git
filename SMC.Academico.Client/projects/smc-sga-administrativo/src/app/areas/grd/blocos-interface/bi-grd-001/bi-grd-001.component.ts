import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'sga-bi-grd-001',
  templateUrl: './bi-grd-001.component.html',
  standalone: false,
})
export class BiGrd001Component implements OnInit {
  /**
   * @Description Campos do objetos dados da divisao
   * Ex:
   * divisaoSelecionada = {
      cargaHorariaGrade: "60",
      cargaHorariaLancada: "58",
      inicioPeriodoLetivo: "01/08/2020",
      fimPeriodoLetivo: "28/02/2020",
      tipoDistribuicaoAula: "Semanal",
      tipoPulaFeriado: "Não Pula",
      aulaSabado: "false",
      dscTabelaHorario: 'Horário Padrão Graduação'
    }
   *
   * Input do bi_grd_001.component
   */
  @Input('s-dados') dados: any;

  constructor() {}

  ngOnInit(): void {}
}
