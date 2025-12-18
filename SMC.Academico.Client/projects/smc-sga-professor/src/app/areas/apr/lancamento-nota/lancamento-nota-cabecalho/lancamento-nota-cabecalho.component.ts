import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { PoModalComponent, PoModalAction } from '@po-ui/ng-components';

@Component({
  selector: 'app-lancamento-nota-cabecalho',
  templateUrl: './lancamento-nota-cabecalho.component.html',
  standalone: false,
})
export class LancamentoNotaCabecalhoComponent implements OnInit {
  constructor(private sanitizer: DomSanitizer) {}

  get descricaoOrigemAvaliacaoSafeHtml(): SafeHtml {
    return this.sanitizer.bypassSecurityTrustHtml(this.descricaoOrigemAvaliacao);
  }
  @Input() descricaoOrigemAvaliacao: string;
  @Input() nomeAluno: string;
  @Input() mostrarComandoMateriaLecionada: boolean;
  @Input() diarioFechado: boolean;
  @Input() materiaLecionada: string;
  @Input() permiteMateriaLecionada: boolean;
  @Input() processandoMateriaLecionada: boolean;
  @Output() valorMateriaLecionada = new EventEmitter();
  @ViewChild('modalMateriaLecionada') modalMateriaLecionada: PoModalComponent;
  materiaBanco: string;

  ngOnInit() {
    this.materiaBanco = this.materiaLecionada;
  }

  // tslint:disable-next-line: member-ordering
  acaoFecharModalMateria: PoModalAction = {
    action: () => {
      this.materiaLecionada = this.materiaBanco;
      this.modalMateriaLecionada.close();
    },
    label: 'Cancelar',
    danger: true,
  };

  // tslint:disable-next-line: member-ordering
  acaoSalvarModalMateria: PoModalAction = {
    action: () => {
      this.valorMateriaLecionada.emit(this.materiaLecionada);
      this.modalMateriaLecionada.close();
    },
    label: 'Salvar',
  };
}
