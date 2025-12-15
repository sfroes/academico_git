import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { conditionalValidator } from 'projects/shared/validators/smc-validator.directive';
import { GradeHorarioCompartilhadoAddComponent } from '../grade-horario-compartilhado-add/grade-horario-compartilhado-add.component';

@Component({
  selector: 'sga-grade-horario-compartilhada-filtro',
  templateUrl: './grade-horario-compartilhada-filtro.component.html',
  styles: [],
})
export class GradeHorarioCompartilhadaFiltroComponent implements OnInit {
  formFiltro: FormGroup;
  @ViewChild(GradeHorarioCompartilhadoAddComponent) modalGradeAdd: GradeHorarioCompartilhadoAddComponent;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.formFiltro = this.inicializarFormFiltro();
  }

  onSubmit() {
    // const modelo: SituacaoAulaLoteFiltroModel = {
    //   seqsColaborador: this.formFiltro.value.seqColaborador,
    //   seqCicloLetivo: this.formFiltro.value.seqCicloLetivo,
    //   seqsDivisaoTurma: this.formFiltro.value.seqsDivisao,
    //   seqTurma: this.formFiltro.value.seqTurma,
    //   fimPeriodo: this.formFiltro.value.dataFim,
    //   inicioPeriodo: this.formFiltro.value.dataInicio,
    //   situacaoApuracaoFrequencia: this.formFiltro.value.situacaoAula,
  }

  limparForm() {
    this.formFiltro.reset();
    // this.dataService.listaAulas = [];
    // this.dataService.$atualizarListaAulas.next();
  }

  adicionarCompartilhamento() {
    this.modalGradeAdd.modal.open();
  }

  private inicializarFormFiltro() {
    return this.fb.group({
      seqCicloLetivo: [null, Validators.required],
      seqTurma: [null, Validators.required],
      dataInicio: [
        null,
        conditionalValidator(() => !isNullOrEmpty(this.formFiltro?.value.dataFim), Validators.required),
      ],
      dataFim: [
        null,
        conditionalValidator(() => !isNullOrEmpty(this.formFiltro?.value.dataInicio), Validators.required),
      ],
      seqsDivisao: [null],
      seqColaborador: [null, Validators.required],
      situacaoAula: [null, Validators.required],
    });
  }
}
