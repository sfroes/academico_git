import { Component, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { environment } from 'projects/smc-sga-professor/src/environments/environment';
import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApuracaoFrequenciaDataService } from '../services/apuracao-frequencia-data.service';

@Component({
  selector: 'sga-apuracao-frequencia-grid-itens',
  templateUrl: './apuracao-frequencia-grid-itens.component.html',
})
export class ApuracaoFrequenciaGridItensComponent implements OnInit {
  get alunos$() {
    return this.dataService.model$.pipe(map(m => m.alunos));
  }
  get form() {
    return this.dataService.form;
  }
  private _subs: Subscription[] = [];
  private set subs(value: Subscription) {
    this._subs.push(value);
  }

  get devEnv() {
    return !environment.production;
  }

  constructor(private dataService: ApuracaoFrequenciaDataService) {}

  ngOnInit(): void {
    const sub = this.alunos$.subscribe(s => {
      if (s?.length > 0) {
        this.configurarDependency();
        sub.unsubscribe();
      }
    });
  }

  private configurarDependency() {
    const alunos = this.form.controls.alunos as FormArray;
    alunos.controls.forEach(f => {
      const alunoControl = f as FormGroup;
      this.subs = alunoControl.controls.apuracoes.valueChanges.subscribe(apuracoes => {
        this.atualizarFaltas(alunoControl, apuracoes);
      });
    });
  }

  ngOnDestroy(): void {
    this._subs.forEach(f => f.unsubscribe());
  }

  recuperarApuracao(indexAluno: number, indexApuracao: number) {
    return this.form.get(`alunos.${indexAluno}.apuracoes.${indexApuracao}`);
  }

  recuperarTotal(indexAluno: number) {
    return this.form.get(`alunos.${indexAluno}.total`).value;
  }

  recuperarPercentual(indexAluno: number) {
    return this.form.get(`alunos.${indexAluno}.percentual`).value;
  }

  private atualizarFaltas(form: FormGroup, model: any) {
    let faltas = 0;
    model.forEach(f => {
      if (f.frequencia === 'Ausente' && f.ocorrenciaFrequencia !== 'Abono') {
        faltas++;
      }

      if (f.frequencia === 'Presente' && f.ocorrenciaFrequencia === 'Sanção') {
        faltas++;
      }
    });
    form.controls.total.setValue(faltas);
    form.controls.percentual.setValue(Math.trunc((faltas / this.dataService.model$.value.cargaHoraria) * 100));
  }
}
