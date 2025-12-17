import { Component, OnInit, forwardRef, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { SmcLookupBaseComponent } from '../smc-lookup/smc-lookup-base.component';
import { NG_VALUE_ACCESSOR, FormBuilder, FormGroup } from '@angular/forms';
import { SmcLookupCicloLetivoService } from './smc-lookup-ciclo-letivo.service';
import { PoTableColumn, PoSelectOption } from '@po-ui/ng-components';
import { SmcKeyValueModel } from '../../../models/smc-key-value.model';
import { SmcLoadService } from '../../../services/load/smc-load.service';
import { SmcLookupCacheService } from '../smc-lookup/smc-lookup-cache.service';


@Component({
  selector: 'smc-lookup-ciclo-letivo',
  templateUrl: './smc-lookup-ciclo-letivo.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SmcLookupCicloLetivoComponent),
      multi: true,
    },
  ],
  standalone: false,
})
export class SmcLookupCicloLetivoComponent extends SmcLookupBaseComponent implements OnInit {
  listaBuscaSimples: SmcKeyValueModel[] = [];
  colunasBuscaAvancada: PoTableColumn[] = [
    { property: 'seq', label: 'ID', width:'5%' },
    { property: 'descricao', label: 'Descrição',width: '15%' },
    { property: 'descricaoRegimeLetivo', label: 'Regime letivo', width: '15%' },
    { property: 'descricaoNiveisEnsino', label: 'Nível de ensino', width: '65%' },
  ];

  @Output('s-seqCicloLetivo') seqCicloLetivo = new EventEmitter<string>();
  dataSourceRegimeLetivo: PoSelectOption[] = [];

  constructor(
    private lookupCicloLetivoService: SmcLookupCicloLetivoService,
    private fb: FormBuilder,
    loadingService: SmcLoadService,
    changeDetectorRef: ChangeDetectorRef,
    lookupCacheService: SmcLookupCacheService
  ) {
    super(lookupCicloLetivoService, loadingService, changeDetectorRef, lookupCacheService);
  }

  ngOnInit(): void {
    this.filterForm = this.createForm();
    this.buscarDataSourceRegimeLetivo();
  }

  createForm() {
    return this.fb.group({
      ano: [],
      numero: [],
      seqRegimeLetivo: [],
    });
  }

  async buscarDataSourceRegimeLetivo() {
    try {
      const result = await this.lookupCicloLetivoService.dataSourceRegimeLetivo();
      this.dataSourceRegimeLetivo = result || [];
      this.changeDetectorRef.detectChanges();
    } catch (error) {
      console.error('Erro ao buscar regime letivo:', error);
      this.dataSourceRegimeLetivo = [];
    }
  }
}
