import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PrimeNgMultiselectOptionsModel } from 'projects/shared/models/primeng-multiselect-options.model';
import { BehaviorSubject, Subject, Subscription } from 'rxjs';
import { ApuracaoFrequenciaModel } from '../models/apuracao-frequencia.model';

@Injectable({
  providedIn: 'root',
})
export class ApuracaoFrequenciaDataService {
  private _model$ = new BehaviorSubject<ApuracaoFrequenciaModel>({
    descricaoOrigemAvaliacao: 'Carregando...',
    dataLimite: null,
    quantidadeDiasPrazoApuracaoFrequencia: 0,
    quantidadeMinutosPrazoAlteracaoFrequencia: 0,
    cargaHoraria: 0,
    usuarioAutenticado: null,
    aulas: [],
    alunos: [],
    dias: [],
  });

  get model$() {
    return this._model$;
  }

  get modelSnapshot() {
    let sub: Subscription;
    let data: ApuracaoFrequenciaModel;
    try {
      sub = this.model$.subscribe(value => {
        data = value;
      });
    } finally {
      sub.unsubscribe();
    }
    return data;
  }
  emApuracao$ = new BehaviorSubject<number>(0);
  emCancelamento$ = new BehaviorSubject<number>(0);
  diaNotificacao$ = new Subject<PrimeNgMultiselectOptionsModel>();

  _dataSourceSituacaoApuracaoFrequencia: PrimeNgMultiselectOptionsModel[] = [
    { code: 'Não apurada', name: 'Aula não apurada' },
    { code: 'Em apuração', name: 'Aula em apuração' },
    { code: 'Executada', name: 'Aula executada' },
    { code: 'Não executada', name: 'Aula não executada' },
  ];

  get dataSourceSituacaoApuracaoFrequencia() {
    return this._dataSourceSituacaoApuracaoFrequencia;
  }

  private _form: FormGroup;

  get form() {
    return this._form;
  }

  private _dataHoraServidor: Date;

  get dataHoraServidor() {
    return this._dataHoraServidor;
  }

  set dataHoraServidor(value: Date) {
    this._dataHoraServidor = value;
  }

  constructor(private fb: FormBuilder) {
    this.criarForm();
  }

  private criarForm() {
    this._form = this.fb.group({
      alunos: this.fb.array([]),
    });
  }

  private _processando$ = new BehaviorSubject<boolean>(false);

  get processando$() {
    return this._processando$;
  }

  private _horarioLimiteTurno$ = new BehaviorSubject<string>('');

  get horarioLimiteTurno$() {
    return this._horarioLimiteTurno$;
  }
}
