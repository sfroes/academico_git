import { Component, OnInit, Output, ViewChild, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { PoTableColumn } from '@po-ui/ng-components';
import { SmcLookupCacheService } from 'projects/shared/components/lookups/smc-lookup/smc-lookup-cache.service';
import { SmcModalComponent } from '../../../../../../../shared/components/smc-modal/smc-modal.component';
import { EventoAulaDivisaoTurmaModel } from '../../moldels/evento-aula-divisao-turma.model';
import { EventoAulaSimulacaoModel } from '../../moldels/evento-aula-simulacao.model';
import { EventoAulaModel } from '../../moldels/evento-aula.model';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';

@Component({
  selector: 'sga-evento-aula-agendamento-simulacao',
  templateUrl: './evento-aula-agendamento-simulacao.component.html',
})
export class EventoAulaAgendamentoSimulacaoComponent implements OnInit {
  colunasTabela: PoTableColumn[] = [
    { property: 'seq', label: 'seq', visible: false },
    { property: 'seqDivisaoTurma', label: 'seq', visible: false },
    { property: 'diaSemanaFormatada', label: 'Data aula' },
    { property: 'horaInicio', label: 'Hora inicio' },
    { property: 'horaFim', label: 'Hora fim' },
    { property: 'descricaoColaboradores', label: 'Professor(es)' },
    { property: 'local', label: 'Local da aula' },
  ];
  divisaoTurma: EventoAulaDivisaoTurmaModel;
  formVisualizacao: FormGroup;
  eventosAula: EventoAulaModel[] = [];
  exibir = false;
  @Output('s-submit-simulacao') onSubmitSimulacao = new EventEmitter();
  @ViewChild(SmcModalComponent) modal: SmcModalComponent;

  constructor(
    private service: EventoAulaService,
    private dataService: EventoAulaDataService,
    public lookupCache: SmcLookupCacheService
  ) {}

  ngOnInit(): void {}

  gerarSimulacao(form: FormGroup) {
    this.exibir = true;
    this.divisaoTurma = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
      f => f.seq === form.get('seqDivisaoTurma').value
    );
    this.formVisualizacao = form;
    this.montarTabelaHorarios();
    this.modal.open();
  }

  montarTabelaHorarios() {
    const parametros: EventoAulaSimulacaoModel = {
      seqDivisaoTurma: this.formVisualizacao.get('seqDivisaoTurma').value,
      seqsHorarios: this.formVisualizacao.get('horarios').value as [],
      descricaoDivisao: this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
        f => f.seq === this.formVisualizacao.get('seqDivisaoTurma').value
      ).descricaoDivisaoFormatada,
      idtTipoRecorrencia: this.formVisualizacao.get('idtTipoRecorrencia').value,
      idtTipoInicioRecorrencia: this.formVisualizacao.get('idtTipoInicioRecorrencia').value,
      comeca: this.formVisualizacao.get('comeca').value,
      termina: this.formVisualizacao.get('termina').value,
      repetir: this.formVisualizacao.get('repetir').value,
      local: this.formVisualizacao.get('local').value,
      colaboradores: this.formVisualizacao.get('colaboradores').value,
    };

    const eventos = this.service.gerarSimulacaoEventos(parametros);
    const colaboradores = this.dataService.colaboradoresTurma;
    eventos.forEach(
      e =>
        (e.descricaoColaboradores = e.colaboradores
          ?.filter(f => f.seqColaborador)
          .map(m => colaboradores.find(f => f.seq === m.seqColaborador).nome)
          .sort()
          .join(', '))
    );

    this.eventosAula = eventos;
  }
  salvarSimulacao() {
    this.fecharSimulacao();
    this.onSubmitSimulacao.emit();
  }

  fecharSimulacao() {
    this.eventosAula = [];
    this.modal.close();
  }
}
