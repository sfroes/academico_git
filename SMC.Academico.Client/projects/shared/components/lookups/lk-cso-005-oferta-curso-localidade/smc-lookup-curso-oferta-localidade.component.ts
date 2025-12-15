import { ChangeDetectorRef, Component, forwardRef, Input, OnInit } from '@angular/core';
import { FormBuilder, NG_VALUE_ACCESSOR } from '@angular/forms';
import { PoMultiselectOption, PoTableColumn } from '@po-ui/ng-components';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { SmcLookupBaseComponent } from '../smc-lookup/smc-lookup-base.component';
import { SmcLookupCacheService } from '../smc-lookup/smc-lookup-cache.service';
import { SmcLookupCursoOfertaLocalidadeService } from './smc-lookup-curso-oferta-localidade.service';

@Component({
  selector: 'smc-lookup-curso-oferta-localidade',
  templateUrl: './smc-lookup-curso-oferta-localidade.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SmcLookupCursoOfertaLocalidadeComponent),
      multi: true,
    },
  ],
})
export class SmcLookupCursoOfertaLocalidadeComponent extends SmcLookupBaseComponent implements OnInit {
  colunasBuscaAvancada: PoTableColumn[] = [
    { property: 'seq', visible: false },
    { property: 'descricaoNivelEnsino', label: 'Nível de ensino' },
    { property: 'descricaoCurso', label: 'Curso' },
    { property: 'descricaoOfertaCurso', label: 'Oferta de curso' },
    { property: 'ativo', label: 'Ativo', type: 'boolean' },
    { property: 'nomeLocalidade', label: 'Localidade' },
    { property: 'descricaoModalidade', label: 'Modalidade' },
  ];
  dataSourceEntidadesResponsaveis: PoMultiselectOption[] = [];
  dataSourceNivelEnsino: SmcKeyValueModel[] = [];
  dataSourceSituacao: SmcKeyValueModel[] = [];
  dataSourceTiposFormacaoEspecifica: SmcKeyValueModel[] = [];
  dataSourceGrauAcademico: SmcKeyValueModel[] = [];
  dataSourceLocalidade: SmcKeyValueModel[] = [];
  dataSourceModalidade: SmcKeyValueModel[] = [];
  @Input('s-label') label = 'Oferta de curso';
  @Input('s-seq-localidade') seqLocalidade: string = null;
  @Input('s-seq-curso') seqCurso: string = null;
  @Input('s-nome-curso') nomeCurso: string = null;
  @Input('s-seqs-entidade-responsavel') seqsEntidadesResponsaveis: string[] = null;
  @Input('s-seqs-nivel-ensino') seqNivelEnsino: string[] = null;
  @Input('s-seq-situacao-atual') seqSituacaoAtual: string = null;
  @Input('s-seq-tipo-formacao-especifica') seqTipoFormacaoEspecifica: string = null;
  @Input('s-seq-grau-academico') seqGrauAcademico: string = null;
  @Input('s-seq-grau-academico') seqModalidade: string = null;

  constructor(
    private fb: FormBuilder,
    private cursoOfertaLocalidadeSevice: SmcLookupCursoOfertaLocalidadeService,
    loadService: SmcLoadService,
    changeDetectionRef: ChangeDetectorRef,
    lookupCacheService: SmcLookupCacheService
  ) {
    super(cursoOfertaLocalidadeSevice, loadService, changeDetectionRef, lookupCacheService);
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.configurarDependency();
    this.preencherDataSourceEntidadesResponsaveis();
    this.preencherDataSoureNivelEnsino();
    this.preencherDataSourceSituacao();
    this.preencherDatasourceTiposFormacaoEspecifica(null);
    this.preencherDataSourceLocalidade(null);
    this.preencherDataSourceModalidade(null);
  }

  seletor(linha: any): SmcKeyValueModel {
    return {
      key: linha.seq,
      value: linha.descricaoOfertaCurso,
    };
  }

  protected createForm() {
    this.filterForm = this.fb.group({
      seqCurso: [this.seqCurso],
      nomeCurso: [this.nomeCurso],
      seqsEntidadesResponsaveis: [this.seqsEntidadesResponsaveis],
      seqNivelEnsino: [this.seqNivelEnsino],
      seqSituacaoAtual: [this.seqSituacaoAtual],
      seqTipoFormacaoEspecifica: [this.seqTipoFormacaoEspecifica],
      seqGrauAcademico: [this.seqGrauAcademico],
      seqLocalidade: [this.seqLocalidade],
      seqModalidade: [this.seqModalidade],
    });
  }

  private configurarDependency() {
    this.filterForm.controls.seqNivelEnsino.valueChanges.subscribe(seqNivelEnsino => {
      const preenchido = seqNivelEnsino?.length;
      if (!preenchido) {
        this.filterForm.controls.seqSituacaoAtual.setValue('');
      }
      this.preencherDatasourceTiposFormacaoEspecifica(seqNivelEnsino);
      this.preencherDataSourceLocalidade(seqNivelEnsino);
      this.preencherDataSourceModalidade(seqNivelEnsino);
    });
  }

  private async preencherDataSourceEntidadesResponsaveis() {
    this.dataSourceEntidadesResponsaveis =
      await this.cursoOfertaLocalidadeSevice.buscarDataSourceEntidadesResponsaveis();
  }

  private async preencherDataSoureNivelEnsino() {
    this.dataSourceNivelEnsino = await this.cursoOfertaLocalidadeSevice.buscarDataSourceNivelEnsino();
  }

  private async preencherDataSourceSituacao() {
    this.dataSourceSituacao = await this.cursoOfertaLocalidadeSevice.buscarDataSourceSituacao();
  }

  private async preencherDatasourceTiposFormacaoEspecifica(seqNivelEnsino: string) {
    this.dataSourceTiposFormacaoEspecifica =
      await this.cursoOfertaLocalidadeSevice.buscarDataSourceTiposFormacaoEspecifica(seqNivelEnsino);
  }

  private async preencherDataSourceLocalidade(seqNivelEnsino: string) {
    this.dataSourceLocalidade = await this.cursoOfertaLocalidadeSevice.buscarDataSourceLocalidade(seqNivelEnsino);
  }

  private async preencherDataSourceModalidade(seqNivelEnsino: string) {
    this.dataSourceModalidade = await this.cursoOfertaLocalidadeSevice.buscarDataSourceMoadalidde(seqNivelEnsino);
  }
}
