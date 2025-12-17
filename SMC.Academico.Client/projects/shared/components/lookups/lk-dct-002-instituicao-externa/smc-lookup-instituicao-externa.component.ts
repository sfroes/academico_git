import { Component, OnInit, forwardRef, Output, EventEmitter, ChangeDetectorRef, Input } from '@angular/core';
import { SmcLookupBaseComponent } from '../smc-lookup/smc-lookup-base.component';
import { NG_VALUE_ACCESSOR, FormBuilder, FormGroup } from '@angular/forms';
import { SmcLookupInstituicaoExternaService } from './smc-lookup-instituicao-externa.service';
import { PoTableColumn, PoSelectOption, InputBoolean } from '@po-ui/ng-components';
import { SmcKeyValueModel } from '../../../models/smc-key-value.model';
import { SmcLoadService } from '../../../services/load/smc-load.service';
import { dataSourceBoolean } from '../../../utils/util';
import { SmcLookupCacheService } from '../smc-lookup/smc-lookup-cache.service';

@Component({
  selector: 'smc-lookup-instituicao-externa',
  templateUrl: './smc-lookup-instituicao-externa.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SmcLookupInstituicaoExternaComponent),
      multi: true,
    },
  ],
  standalone: false,
})
export class SmcLookupInstituicaoExternaComponent extends SmcLookupBaseComponent implements OnInit {
  listaBuscaSimples: SmcKeyValueModel[] = [];
  colunasBuscaAvancada: PoTableColumn[] = [
    { property: 'seq', label: 'ID' },
    { property: 'sigla', label: '	Sigla' },
    { property: 'nome', label: 'Nome' },
    { property: 'descricaoPais', label: 'País' },
    { property: 'tipoInstituicaoEnsino', label: 'Tipo' },
    { property: 'descricaoCategoria', label: 'Categoria' },
    { property: 'ativo', label: 'Ativo?', type: 'boolean' },
  ];
  dataSourcePaisesValidosCorreios: SmcKeyValueModel[] = [];
  dataSourceBoolean: SmcKeyValueModel[] = [];
  dataSourceTipoInstituicaoEnsino: SmcKeyValueModel[] = [];
  dataSourceCategoriasInstituicaoEnsino: SmcKeyValueModel[] = [];
  @Input('s-retornar-instituicao-ensinoLogada') retornarInstituicaoEnsinoLogada = false;
  @Input('s-listar-somente-instituicoes-ensino') listarSomenteInstituicoesEnsino = false;
  @Input('s-label') label = 'Instituição';

  constructor(
    private lookupInstituicaoExternaService: SmcLookupInstituicaoExternaService,
    private fb: FormBuilder,
    loadingService: SmcLoadService,
    changeDetectorRef: ChangeDetectorRef,
    lookupCacheService: SmcLookupCacheService
  ) {
    super(lookupInstituicaoExternaService, loadingService, changeDetectorRef, lookupCacheService);
  }

  ngOnInit(): void {
    this.filterForm = this.createForm();
    this.buscarDataSourceCategoriasInstituicaoEnsino();
    this.buscarDataSourcePaisesValidosCorreios();
    this.buscarDataSourceTipoInstituicaoEnsino();
    this.buscarDataSourceBoolean();
  }

  seletor(linha: any): SmcKeyValueModel {
    return {
      key: linha.seq,
      value: linha.nome,
    };
  }

  createForm() {
    return this.fb.group({
      retornarInstituicaoEnsinoLogada: [this.retornarInstituicaoEnsinoLogada],
      listarSomenteInstituicoesEnsino: [this.listarSomenteInstituicoesEnsino],
      sigla: [],
      nome: [],
      codigoPais: [],
      tipoInstituicaoEnsino: [],
      seqCategoriaInstituicaoEnsino: [],
      ativo: [],
    });
  }

  async buscarDataSourceCategoriasInstituicaoEnsino() {
    this.dataSourceCategoriasInstituicaoEnsino = await this.lookupInstituicaoExternaService.dataSourceCategoriasInstituicaoEnsino();
  }

  async buscarDataSourcePaisesValidosCorreios() {
    this.dataSourcePaisesValidosCorreios = await this.lookupInstituicaoExternaService.dataSourcePaisesValidosCorreios();
  }

  async buscarDataSourceTipoInstituicaoEnsino() {
    this.dataSourceTipoInstituicaoEnsino = await this.lookupInstituicaoExternaService.dataSourceTipoInstituicaoEnsino();
  }

  buscarDataSourceBoolean() {
    this.dataSourceBoolean = dataSourceBoolean(true);
  }
}
