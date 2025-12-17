import { Component, OnInit } from '@angular/core';
import moment from 'moment';
import { PrimeNgMultiselectOptionsModel } from 'projects/shared/models/primeng-multiselect-options.model';
import { distinctArray } from 'projects/shared/utils/util';
import { ApuracaoFrequenciaDataService } from '../services/apuracao-frequencia-data.service';
import { ApuracaoFrequenciaAulaModel } from './../models/apuracao-frequencia-aula.model';

@Component({
  selector: 'sga-apuracao-frequencia-notificacoes',
  templateUrl: './apuracao-frequencia-notificacoes.component.html',
  styles: [],
})
export class ApuracaoFrequenciaNotificacoesComponent implements OnInit {
  exibir = false;
  listaDatasExpiradas: string[] = [];
  listaDatasDentroPrazo: string[] = [];

  get quantidadeDiasPrazoApuracaoFrequencia(): number {
    return this.dataService.model$.value.quantidadeDiasPrazoApuracaoFrequencia;
  }

  constructor(private dataService: ApuracaoFrequenciaDataService) {}

  ngOnInit(): void {
    this.dataService.model$.subscribe(data => {
      const aulasNaoApuradas = data.aulas.filter(m => m.situacaoApuracaoFrequencia === 'Não apurada');
      this.listaDatasExpiradas = this.formatarNotificacoes(
        aulasNaoApuradas.filter(m => this.dataService.dataHoraServidor > m.dataLimiteApuracaoFrequencia)
      );
      this.listaDatasDentroPrazo = this.formatarNotificacoes(
        aulasNaoApuradas.filter(m => this.dataService.dataHoraServidor <= m.dataLimiteApuracaoFrequencia)
      );
    });
  }

  exibirNotificacao() {
    this.exibir = !this.exibir;
  }

  selecionarDia(data: string) {
    const dataSource: PrimeNgMultiselectOptionsModel = { code: data, name: data };
    this.dataService.diaNotificacao$.next(dataSource);
    this.exibir = false;
  }

  private formatarNotificacoes(datas: ApuracaoFrequenciaAulaModel[]): string[] {
    return distinctArray(datas.map(m => m.dataFormatada))
      .sort((a, b) => b.localeCompare(a))
      .map(m => moment(m, 'YYYYMMDD').format('DD/MM/YYYY'));
  }
}
