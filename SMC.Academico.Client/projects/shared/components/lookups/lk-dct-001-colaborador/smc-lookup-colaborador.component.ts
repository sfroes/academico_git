import { ChangeDetectorRef, Component, EventEmitter, forwardRef, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, NG_VALUE_ACCESSOR } from '@angular/forms';
import { PoInputComponent, PoSelectOption, PoTableColumn } from '@po-ui/ng-components';
import { SmcKeyValueModel } from '../../../models/smc-key-value.model';
import { SmcLookupColaboradorService } from './smc-lookup-colaborador.service';
import { SmcLoadService } from '../../../services/load/smc-load.service';
import { SmcLookupBaseComponent } from '../smc-lookup/smc-lookup-base.component';
import { isNullOrEmpty } from '../../../utils/util';
import { SmcLookupCacheService } from '../smc-lookup/smc-lookup-cache.service';

@Component({
  selector: 'smc-lookup-colaborador',
  templateUrl: './smc-lookup-colaborador.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SmcLookupColaboradorComponent),
      multi: true,
    },
  ],
})
export class SmcLookupColaboradorComponent extends SmcLookupBaseComponent implements OnInit {
  colunasBuscaAvancada: PoTableColumn[] = [
    { property: 'seq', label: 'ID' },
    { property: 'nome', label: 'Nome' },
    { property: 'nomeSocial', label: 'Nome Social' },
    { property: 'sexo', label: 'Sexo' },
    { property: 'dataNascimento', label: 'Data de nascimento' },
    { property: 'cpf', label: 'CPF' },
    { property: 'passaporte', label: 'Passaporte' },
  ];
  listaBuscaSimples: SmcKeyValueModel[] = [];
  situacaoColaboradorReadonly = true;
  dataSourceEntidadeResposavel: SmcKeyValueModel[] = [];
  dataSourceTipoVinculo: SmcKeyValueModel[] = [];
  dataSourceTipoAtividade: SmcKeyValueModel[] = [];
  dataSourceSituacaoColaboradorInstituicao: SmcKeyValueModel[] = [];
  seqEntidadeVinculo: string[] = null;
  @Output('s-seqColaborador') seqColaborador = new EventEmitter<string>();
  @Input('s-label') label = '';
  private _seqTurma?: string = '';

  @Input('seq-turma') set seqTurma(value: string) {
    this.lookupColaboradorService.seqTruma = value;
    this.filterForm?.controls.seqTurma.setValue(value);
    this._seqTurma = value;
  }

  get seqTurma() {
    return this._seqTurma;
  }

  constructor(
    private lookupColaboradorService: SmcLookupColaboradorService,
    private fb: FormBuilder,
    loadingService: SmcLoadService,
    changeDetectorRef: ChangeDetectorRef,
    lookupCacheService: SmcLookupCacheService
  ) {
    super(lookupColaboradorService, loadingService, changeDetectorRef, lookupCacheService);
  }

  ngOnInit(): void {
    this.filterForm = this.createForm();
    this.buscarDataSourceEntidadeResponsavel();
    this.buscarDataSourceTipoVinculo();
    this.bucacarDataSourceTipoAtividade();
    this.bucacarDataSourceSituacaoColaboradorInstituicao();
    this.configurarDependecy();
  }

  seletor(linha: any): SmcKeyValueModel {
    return {
      key: linha.seq,
      value: linha.nome,
    };
  }

  createForm() {
    return this.fb.group({
      seq: [],
      nome: [],
      nomeSocial: [],
      seqEntidadeVinculo: [],
      seqTipoVinculoColaborador: [],
      dataInicio: [],
      dataFim: [],
      seqCursoOfertaLocalidade: [],
      tipoAtividade: [null],
      seqInstituicaoExterna: [],
      situacaoColaboradorNaInstituicao: [],
      seqTurma: [],
    });
  }

  async buscarDataSourceEntidadeResponsavel() {
    this.dataSourceEntidadeResposavel = await this.lookupColaboradorService.dataSourceEntidadeResposavel();
  }

  async buscarDataSourceTipoVinculo() {
    this.dataSourceTipoVinculo = await this.lookupColaboradorService.dataSourceTipoVinculo();
  }

  async bucacarDataSourceTipoAtividade() {
    this.dataSourceTipoAtividade = await this.lookupColaboradorService.dataSourceTipoAtividade();
  }

  async bucacarDataSourceSituacaoColaboradorInstituicao() {
    this.dataSourceSituacaoColaboradorInstituicao =
      await this.lookupColaboradorService.dataSourceSituacaoColaboradorInstituicao();
  }

  configurarDependecy() {
    this.filterForm.controls.seqInstituicaoExterna.valueChanges.subscribe(seqInstituicaoExterna => {
      if (isNullOrEmpty(seqInstituicaoExterna)) {
        this.filterForm.get('situacaoColaboradorNaInstituicao').setValue('');
        this.situacaoColaboradorReadonly = true;
      } else {
        this.situacaoColaboradorReadonly = false;
      }
    });
    this.filterForm.controls.seqEntidadeVinculo.valueChanges.subscribe(seqEntidadeVinculo => {
      this.seqEntidadeVinculo = isNullOrEmpty(seqEntidadeVinculo) ? null : [seqEntidadeVinculo];
    });
  }
}
