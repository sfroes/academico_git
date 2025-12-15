import { ValidatorFn, AbstractControl, ValidationErrors, AsyncValidatorFn, FormGroup } from '@angular/forms';
import * as moment from 'moment';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { Observable, of } from 'rxjs';
import { delay, map, tap } from 'rxjs/operators';
import { EventoAulaDivisaoTurmaModel } from '../moldels/evento-aula-divisao-turma.model';
import { EventoAulaService } from '../services/evento-aula.service';

export function diaUtil(service: EventoAulaService, divisao: EventoAulaDivisaoTurmaModel): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const data: Date = moment(control.value, 'YYYY-MM-DD').toDate();
    const diaUtil = service.validarDiaUtil(data, divisao);
    return diaUtil ? null : { naoDiaUtil: true };
  };
}

export function vinculoAtivoColaborador(
  service: EventoAulaService,
  recuperarPeriodo: () => { dataInicio: Date; dataFim: Date }
): AsyncValidatorFn {
  return (control: FormGroup): Promise<ValidationErrors | null> => {
    // return of(null)
    //   .pipe(
    //     delay(5000),
    //     tap(() => console.log('delay 5000')),
    //     map(m => {
    //       return null;
    //     })
    //   )
    //   .toPromise();
    const seqColaborador = control.controls.seqColaborador.value as string;
    const seqColaboradorSubstituto = control.controls.seqColaboradorSubstituto.value as string;
    const periodo = recuperarPeriodo();
    let validacao: Observable<boolean>;
    if (isNullOrEmpty(seqColaborador)) {
      return null;
    } else if (!isNullOrEmpty(seqColaboradorSubstituto)) {
      validacao = service.validarVinculoColaboradorPeriodo(
        seqColaboradorSubstituto,
        periodo.dataInicio,
        periodo.dataFim
      );
    } else {
      validacao = service.validarVinculoColaboradorPeriodo(seqColaborador, periodo.dataInicio, periodo.dataFim);
    }
    return validacao.pipe(map(valido => (valido ? null : { semVinculoPeriodo: true }))).toPromise();
  };
}
