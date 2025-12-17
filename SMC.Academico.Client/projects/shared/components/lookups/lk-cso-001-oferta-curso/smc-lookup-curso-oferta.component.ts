import { ChangeDetectorRef, Component, forwardRef, Input, OnInit } from '@angular/core';
import { FormBuilder, NG_VALUE_ACCESSOR } from '@angular/forms';
import { PoMultiselectOption, PoSelectOption, PoTableColumn } from '@po-ui/ng-components';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { SmcLookupBaseComponent } from '../smc-lookup/smc-lookup-base.component';
import { SmcLookupCacheService } from '../smc-lookup/smc-lookup-cache.service';
import { SmcLookupCursoOfertaService } from './smc-lookup-curso-oferta.service';

@Component({
  selector: 'smc-lookup-curso-oferta',
  templateUrl: './smc-lookup-curso-oferta.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SmcLookupCursoOfertaComponent),
      multi: true,
    },
  ],
  standalone: false,
})
export class SmcLookupCursoOfertaComponent extends SmcLookupBaseComponent implements OnInit {
  colunasBuscaAvancada: PoTableColumn[] = [
    { property: 'descricaoInstituicaoNivelEnsino', label: 'Nível de ensino', width: '15%' },
    { property: 'descricaoCurso', label: 'Curso', width: '30%' },
    { property: 'descricaoOfertaCurso', label: 'Oferta de curso', width: '50%' },
    { property: 'ativo', label: 'Ativo?', type: 'boolean', width: '5%' },
  ];
  dataSourceEntidadesResponsaveis: PoMultiselectOption[] = [];
  dataSourceNivelEnsino: PoMultiselectOption[] = [];
  dataSourceSituacao: SmcKeyValueModel[] = [];
  dataSourceTiposFormacaoEspecifica: SmcKeyValueModel[] = [];
  dataSourceGrauAcademico: SmcKeyValueModel[] = [];
  dataSourceAtivo: SmcKeyValueModel[] = [];
  @Input('s-label') label = 'Oferta de curso';
  @Input('s-seq-localidade') seqLocalidade: string = null;
  @Input('s-seq-curso') seqCurso: string = null;
  @Input('s-nome-curso') nome: string = null;
  @Input('s-descricao-oferta') descricao: string = null;
  @Input('s-seqs-entidade-responsavel') seqsEntidadesResponsaveis: string[] = null;
  @Input('s-seqs-nivel-ensino') seqNivelEnsino: string[] = null;
  @Input('s-seq-situacao-atual') seqSituacaoAtual: string = null;
  @Input('s-seq-tipo-formacao-especifica') seqTipoFormacaoEspecifica: string = null;
  @Input('s-seq-grau-academico') seqGrauAcademico: string = null;
  @Input('s-ativo') ativo: string = null;

  constructor(
    private fb: FormBuilder,
    private cursoOfertaSevice: SmcLookupCursoOfertaService,
    loadService: SmcLoadService,
    changeDetectionRef: ChangeDetectorRef,
    lookupCacheService: SmcLookupCacheService
  ) {
    super(cursoOfertaSevice, loadService, changeDetectionRef, lookupCacheService);
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.configurarDependency();
    this.preencherDataSourceEntidadesResponsaveis();
    this.preencherDataSoureNivelEnsino();
    this.preencherDataSourceSituacao();
    this.preencherDatasourceTiposFormacaoEspecifica(null);
    // this.preencherDataSourceGrauAcademico(null);
    this.preencherDataSourceAtivo();
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
      nome: [this.nome],
      seqsEntidadesResponsaveis: [this.seqsEntidadesResponsaveis],
      seqNivelEnsino: [this.seqNivelEnsino],
      seqSituacaoAtual: [this.seqSituacaoAtual],
      seqTipoFormacaoEspecifica: [this.seqTipoFormacaoEspecifica],
      seqGrauAcademico: [this.seqGrauAcademico],
      seqLocalidade: [this.seqLocalidade],
      descricao: [this.descricao],
      ativo: [this.ativo],
    });
  }

  private configurarDependency() {
    this.filterForm.controls.seqNivelEnsino.valueChanges.subscribe(seqNivelEnsino => {
      const preenchido = seqNivelEnsino?.length;
      if (!preenchido) {
        this.filterForm.controls.seqSituacaoAtual.setValue('');
      }
      this.preencherDatasourceTiposFormacaoEspecifica(seqNivelEnsino);
      // this.preencherDataSourceGrauAcademico(seqNivelEnsino);
    });
  }

  private async preencherDataSourceEntidadesResponsaveis() {
    this.dataSourceEntidadesResponsaveis = await this.cursoOfertaSevice.buscarDataSourceEntidadesResponsaveis();
  }

  private async preencherDataSoureNivelEnsino() {
    this.dataSourceNivelEnsino = await this.cursoOfertaSevice.buscarDataSourceNivelEnsino();
  }

  private async preencherDataSourceSituacao() {
    this.dataSourceSituacao = await this.cursoOfertaSevice.buscarDataSourceSituacao();
  }

  private async preencherDatasourceTiposFormacaoEspecifica(seqsNivelEnsino: string[]) {
    this.dataSourceTiposFormacaoEspecifica = await this.cursoOfertaSevice.buscarDataSourceTiposFormacaoEspecifica(
      seqsNivelEnsino
    );
  }

  // private async preencherDataSourceGrauAcademico(seqsNivelEnsino: string[]) {
  //   this.dataSourceGrauAcademico = await this.cursoOfertaSevice.buscarDataSourceGrauAcademico(seqsNivelEnsino);
  // }

  private async preencherDataSourceAtivo() {
    this.dataSourceAtivo = [
      { key: 'true', value: 'Sim' },
      { key: 'false', value: 'Não' },
    ];
  }
}
