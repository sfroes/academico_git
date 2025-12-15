import { Component, OnInit, forwardRef, Input, ChangeDetectorRef } from '@angular/core';
import { SmcLookupTurmaService } from './smc-lookup-turma.service';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { NG_VALUE_ACCESSOR, FormBuilder } from '@angular/forms';
import { PoTableColumn, PoMultiselectOption } from '@po-ui/ng-components';
import { SmcLookupBaseComponent } from '../smc-lookup/smc-lookup-base.component';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { SmcLookupCacheService } from '../smc-lookup/smc-lookup-cache.service';
import { isNullOrEmpty } from 'projects/shared/utils/util';

@Component({
  selector: 'smc-lookup-turma',
  templateUrl: './smc-lookup-turma.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SmcLookupTurmaComponent),
      multi: true,
    },
  ],
})
export class SmcLookupTurmaComponent extends SmcLookupBaseComponent implements OnInit {
  colunasBuscaAvancada: PoTableColumn[] = [
    { property: 'seq', visible: false },
    { property: 'descricaoCicloLetivoInicio', label: 'Ciclo letivo', width:'5%' },
    { property: 'codigoFormatado', label: 'Turma', width: '10%' },
    { property: 'descricaoConfiguracaoComponente', label: 'Descrição', width: '55%' },
    { property: 'situacaoTurmaAtual', label: 'Situação', width: '10%' },
    { property: 'inicioPeriodoLetivo', label: 'Início periodo letivo', type: 'date', width: '10%' },
    { property: 'fimPeriodoLetivo', label: 'Fim período letivo', type: 'date', width: '10%' },
  ];
  dataSourceEntidadesResponsaveis: PoMultiselectOption[] = [];
  disableCicloLetivo = false;
  private _seqCicloLetivo?: string = '';

  @Input('seq-ciclo-letivo') set seqCicloLetivoInicio(value: string) {
    this.disableCicloLetivo = !isNullOrEmpty(value);
    this.smcLookupTurmaService.seqCicloLetivoInicio = value;
    this.filterForm?.controls.seqCicloLetivoInicio.setValue(value);
    this._seqCicloLetivo = value;
  }

  get seqCicloLetivoInicio() {
    return this._seqCicloLetivo;
  }

  constructor(
    private fb: FormBuilder,
    private smcLookupTurmaService: SmcLookupTurmaService,
    changeDetectorRef: ChangeDetectorRef,
    loadingService: SmcLoadService,
    lookupCacheService: SmcLookupCacheService
  ) {
    super(smcLookupTurmaService, loadingService, changeDetectorRef, lookupCacheService);
  }

  ngOnInit(): void {
    this.filterForm = this.createForm();
    this.buscarDataSourceEntidadesResponsaveis();
  }

  seletor(linha: any): SmcKeyValueModel {
    return {
      key: linha.seq,
      value: linha.descricaoConfiguracaoComponente,
    };
  }

  createForm() {
    return this.fb.group({
      seqCicloLetivoInicio: [],
      seqsEntidadesResponsaveis: [],
      seqCursoOferta: [],
      codigoFormatado: [],
      descricaoConfiguracao: [],
    });
  }

  async buscarDataSourceEntidadesResponsaveis() {
    this.dataSourceEntidadesResponsaveis = await this.smcLookupTurmaService.buscarDataSourceEntidadesResponsaveis();
  }
}
