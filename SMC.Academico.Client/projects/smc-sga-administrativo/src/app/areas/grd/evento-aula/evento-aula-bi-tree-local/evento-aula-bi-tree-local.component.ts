import { DataSource } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm, FormBuilder, FormGroup } from '@angular/forms';
import { SmcModalComponent } from 'projects/shared/components/smc-modal/smc-modal.component';
import { SmcTreeViewService } from 'projects/shared/components/smc-tree-view/services/smc-tree-view.service';
import { SmcTreeViewItem } from 'projects/shared/components/smc-tree-view/smc-tree-view-item/smc-tree-view-item.interface';
import { SmcTreeViewComponent } from 'projects/shared/components/smc-tree-view/smc-tree-view.component';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';

@Component({
  selector: 'sga-evento-aula-bi-tree-local',
  templateUrl: './evento-aula-bi-tree-local.component.html',
  providers: [SmcTreeViewService],
})
export class EventoAulaBiTreeLocalComponent implements OnInit, OnDestroy {
  dataSourceLocal: SmcTreeViewItem[];
  descricaoFormatada: string = '';
  ultimoItemSelecionado: SmcKeyValueModel = null;
  selecionado: SmcTreeViewItem = null;
  formFiltro: FormGroup;
  @Input('s-required') required: boolean = false;
  @Input('s-readonly') readonly: boolean = false;
  @Input('s-label') label: string;
  @Input('s-selected-key') seqSelecioanda: string;
  @Output('s-selected') selected = new EventEmitter<SmcKeyValueModel>();
  @ViewChild(SmcModalComponent) modal: SmcModalComponent;
  @ViewChild(SmcTreeViewComponent) treeView: SmcModalComponent;

  constructor(
    private dataService: EventoAulaDataService,
    private service: EventoAulaService,
    private notificationService: SmcNotificationService,
    private treeService: SmcTreeViewService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.limpar();
    this.formFiltro = this.initialForm();
    if (!isNullOrEmpty(this.seqSelecioanda)) {
      this.marcarItem(this.dataSourceLocal);
    }
  }

  ngOnDestroy(): void {
    this.remarcarArvore(null, this.dataService.dataSourceLocal);
  }

  abrirArvore() {
    this.modal.open();
  }

  async selecionarLocal() {
    if (this.selecionado) {
      this.seqSelecioanda = this.selecionado.value as string;
      this.descricaoFormatada = await this.service.buscarDescricaoLocalAGD(+this.seqSelecioanda);
      this.ultimoItemSelecionado = { key: this.seqSelecioanda, value: this.descricaoFormatada };
      this.selected.next(this.ultimoItemSelecionado);
      this.fecharArvore();
    } else {
      this.notificationService.warning('Selecione ao menos uma opção');
    }
  }

  initialForm() {
    const modelo = this.fb.group({
      label: [''],
    });
    return modelo;
  }

  onSelected(e: SmcTreeViewItem) {
    this.selecionado = e;
  }

  onUnselected() {
    this.selecionado = null;
  }

  fecharArvore() {
    this.modal.close();
    if (this.selecionado?.label !== this.ultimoItemSelecionado?.key) {
      this.dataSourceLocal = this.remarcarArvore(this.ultimoItemSelecionado.key, this.dataSourceLocal);
    }
  }

  remarcarArvore(valorSelecionado: string, arvore: SmcTreeViewItem[]) {
    const result: SmcTreeViewItem[] = [];
    arvore?.forEach(item => {
      item.expanded = item.selected = item.value === valorSelecionado;
      this.remarcarArvore(valorSelecionado, item.subItems);
      result.push(item);
    });
    return result;
  }

  filtrar() {
    const filtro = this.formFiltro.value.label;
    if (!isNullOrEmpty(filtro)) {
      const itens = this.filtrarItens(filtro, this.dataService.dataSourceLocal);
      this.dataSourceLocal = this.filtrarArvore(
        itens,
        this.treeService.clone(this.dataService.dataSourceLocal),
        filtro
      );
    } else {
      this.limpar();
    }
  }

  limpar() {
    this.formFiltro?.reset();
    this.dataSourceLocal = this.treeService.clone(this.dataService.dataSourceLocal);
  }

  filtrarItens(filtro: string, arvore: SmcTreeViewItem[]) {
    let result: string[] = [];
    arvore?.forEach(item => {
      item.subItems?.forEach(f => (f.parent = item));
      result.push(...this.filtrarItens(filtro, item.subItems));
      if (item.label.toLowerCase().includes(filtro.toLowerCase())) {
        result.push(item.value as string);
        let pai = item.parent;
        while (pai) {
          if (result.includes(pai.value as string)) {
            break;
          }
          result.push(pai.value as string);
          pai = pai.parent;
        }
        if (item.subItems) {
          let filhos: string[] = [];
          item.subItems.forEach(f => filhos.push(f.value as string));
          result = this.preecherFilhos(filhos, result, arvore);
        }
      }
    });
    return result;
  }

  preecherFilhos(
    filhos: string[],
    itensSelecionados: string[],
    arvore: SmcTreeViewItem[],
    subItens: SmcTreeViewItem[] = []
  ) {
    filhos.forEach(filho => {
      arvore?.forEach(item => {
        if (subItens.length > 0) {
          subItens.forEach(sub => {
            if (sub.value === filho) {
              if (!itensSelecionados.includes(filho)) {
                itensSelecionados.push(filho);
              }
              if (sub.subItems) {
                let filhosSub: string[] = [];
                sub.subItems.forEach(f => filhosSub.push(f.value as string));
                this.preecherFilhos(filhosSub, itensSelecionados, arvore, sub.subItems);
              }
            }
          });
        } else {
          if (item.value == filho) {
            itensSelecionados.push(filho);
          }
          if (item.subItems) {
            this.preecherFilhos(filhos, itensSelecionados, arvore, item.subItems);
          }
        }
      });
    });
    return itensSelecionados;
  }

  filtrarArvore(itensPermitidos: string[], arvore: SmcTreeViewItem[], filtro: string) {
    const result: SmcTreeViewItem[] = [];
    arvore?.forEach(item => {
      if (itensPermitidos.includes(item.value as string)) {
        item.cssClass = item.label.toLowerCase().includes(filtro.toLowerCase())
          ? 'smc-tree-view-texto-selecionado'
          : '';
        item.expanded = true;
        result.push(item);
        item.subItems = this.filtrarArvore(itensPermitidos, item.subItems, filtro);
      }
    });
    return result;
  }

  clearSelection() {
    this.descricaoFormatada = '';
    this.ultimoItemSelecionado = { key: '', value: '' };
    this.selected.next(this.ultimoItemSelecionado);
    this.dataSourceLocal = this.remarcarArvore(this.ultimoItemSelecionado.key, this.dataSourceLocal);
  }

  private marcarItem(tree: SmcTreeViewItem[]) {
    tree?.forEach(f => {
      f.expanded = f.selected = f.value == this.seqSelecioanda;
      if (f.selected) {
        this.service
          .buscarDescricaoLocalAGD(+this.seqSelecioanda)
          .then(descricao => (this.descricaoFormatada = descricao));
      }
      this.marcarItem(f.subItems);
    });
  }
}
