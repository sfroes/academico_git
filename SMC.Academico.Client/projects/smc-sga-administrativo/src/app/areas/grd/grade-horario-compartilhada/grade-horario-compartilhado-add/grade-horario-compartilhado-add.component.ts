import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SmcModalComponent } from './../../../../../../../shared/components/smc-modal/smc-modal.component';

@Component({
  selector: 'sga-grade-horario-compartilhado-add',
  templateUrl: './grade-horario-compartilhado-add.component.html',
  styles: [],
  standalone: false,
})
export class GradeHorarioCompartilhadoAddComponent implements OnInit {
  formGrade: FormGroup;
  mensagemInformativa: string;
  classeMensagemInformativa:
    | 'smc-sga-mensagem-local-informa'
    | 'smc-sga-mensagem-local-alerta'
    | 'smc-sga-mensagem-local-sucesso'
    | 'smc-sga-mensagem-local-erro' = 'smc-sga-mensagem-local-informa';
  @ViewChild(SmcModalComponent) modal: SmcModalComponent;
  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.formGrade = this.inicializarModelo();
  }

  exibirModalMensagemInformativa() {}
  salvar() {}
  fecharModal() {
    this.modal.close();
  }
  addCompartilhamento() {}
  getCompartilhamento() {
    return [];
  }
  removeCompartilhamento(index: number) {}

  inicializarModelo() {
    const modelo = this.fb.group({
      compartilhamentos: this.fb.array([this.inicialCompartilhamentos()]),
    });
    return modelo;
  }

  inicialCompartilhamentos() {
    return this.fb.group({
      seqDivisoes: [''],
    });
  }
}
