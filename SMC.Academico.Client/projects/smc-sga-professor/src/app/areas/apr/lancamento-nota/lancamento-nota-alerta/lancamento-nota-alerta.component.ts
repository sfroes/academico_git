import { Component, OnInit, ViewChild } from '@angular/core';
import { PoModalComponent, PoModalAction } from '@po-ui/ng-components';

@Component({
    selector: 'app-lancamento-nota-alerta',
    templateUrl: './lancamento-nota-alerta.component.html'
})
export class LancamentoNotaAlertaComponent implements OnInit {
    constructor() { }

    mensagemAlerta: string;
    nomesAlunoAlerta: string[] = [];
    mensagemUnica: string;
    @ViewChild('modalAlerta') modalAlerta: PoModalComponent;

    ngOnInit() {

    }

    abrirModal(mensagemAlerta: string, nomesAlunoAlerta: string[]) {
        this.mensagemAlerta = mensagemAlerta;
        this.nomesAlunoAlerta = nomesAlunoAlerta;
        this.mensagemUnica = null;
        this.modalAlerta.open();
    }

  abrirModalUnica(mensagemUnica: string) {
    this.mensagemAlerta = null;
    this.nomesAlunoAlerta = [];
    this.mensagemUnica = mensagemUnica;
    this.modalAlerta.open();
  }

  abrirModalUnicaComLista(mensagemUnica: string, nomesAlunoAlerta: string[]) {
        this.mensagemAlerta = null;
        this.nomesAlunoAlerta = nomesAlunoAlerta;
        this.mensagemUnica = mensagemUnica;
        this.modalAlerta.open();
    }

    // tslint:disable-next-line: member-ordering
    acaoFecharModalAleta: PoModalAction = {
        action: null,
        label: 'Fechar',
        danger: true,
    };
}
