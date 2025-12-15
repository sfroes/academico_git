import { QueryList } from '@angular/core';
import { FormGroup, ValidationErrors } from '@angular/forms';
import { PoInputComponent } from '@po-ui/ng-components';
import { SmcKeyValueModel } from '../models/smc-key-value.model';

export function isNullOrEmpty(valor: any) {
  return valor === '' || valor === null || valor === undefined;
}

export function distinctArray<T>(array: T[]) {
  return array.filter((item, i, ar) => ar.indexOf(item) === i);
}

// Configuração para navegar entre a linhas do grid com formato nome.coluna.linha, navegando para cima e para baixo
// Exemplo de implementação na Tela Lançamento de Nota
export function configurarNavegacoesLinha(elementos: QueryList<PoInputComponent>) {
  if (elementos.length > 0) {
    elementos.forEach(queryItem => {
      const el = queryItem.el.nativeElement;
      el.onkeydown = e => {
        if (e.key === 'ArrowDown') {
          e.preventDefault();
          focarProxima(el, elementos.toArray(), 1);
        } else if (e.key === 'ArrowUp') {
          e.preventDefault();
          focarProxima(el, elementos.toArray(), -1);
        }
      };
    });
  }
}

function focarProxima(el: any, elementos: PoInputComponent[], incremento: number) {
  const proximoId = recuperarProximoId(el.id, incremento);
  const proximoElemento = elementos.filter(f => f.el.nativeElement.id === proximoId);
  if (proximoElemento.length > 0) {
    proximoElemento[0].focus();
    proximoElemento[0].inputEl.nativeElement.select();
  }
}

function recuperarProximoId(id: string, incremento: number) {
  const partes = id.split('.');
  const linha = +partes[1] + incremento;
  return `${partes[0]}.${linha}.${partes[2]}`;
}

/**
 * @description Incrementa tempo em um data
 *
 * @param date Data que será incrementada: Ex: "2020-08-28"
 *
 * @param time Tempo que será incrementada a data: "10:59:00"
 *
 * @return Date formtada: Ex: 28/08/2020 10:59:00
 */
export function incrementTimeInDate(date: string, time: string): Date {
  const day = parseInt(date.substring(8, 10));
  const month = parseInt(date.substring(5, 7)) - 1;
  const year = parseInt(date.substring(0, 4));

  const hour = parseInt(time.substring(0, 2));
  const minute = parseInt(time.substring(3, 5));
  const second = parseInt(time.substring(6, 8));

  const dateFormat = new Date(year, month, day, hour, minute, second);

  return dateFormat;
}

/**
 * @description Converte um valor qualquer em boolean
 *
 * @param val Valor
 *
 * @return Boolean
 */
export function convertToBoolean(val: any): boolean {
  if (typeof val === 'string') {
    val = val.toLowerCase().trim();
    return val === 'true' || val === 'on';
  }

  if (typeof val === 'number') {
    return val === 1;
  }

  return !!val;
}

/**
 * @description Seletor padrão de um SmcKeyValueModel
 *
 * @param item Item com os campos seq para key e descricao para value
 *
 * @returns SmcKeyValueModel com os valores informados
 */
export function defaultKeyValueSelector(item: any): SmcKeyValueModel {
  return { key: item?.seq, value: item?.descricao };
}

/**
 * @description Valida e o valor de uma string é um numero natural
 *
 * @param str string para o teste
 *
 * @returns true caso o str seja um numero natural
 */
export function isNaturalNumber(str: string): boolean {
  const pattern = /^\d*$/;
  return pattern.test(str);
}

/**
 * @description Datasource padrão para campos boolean
 *
 * @param keyValue define qual o retorno um keyValue or labelValue
 *
 * @return DataSource de boolenano
 */
export function dataSourceBoolean(keyValue: boolean): any {
  if (keyValue) {
    return [
      { key: 'true', value: 'Sim' },
      { key: 'false', value: 'Não' },
    ];
  } else {
    return [
      { label: 'Selecionar...', value: '' },
      { label: 'Sim', value: 'true' },
      { label: 'Não', value: 'false' },
    ];
  }
}

/**
 * @description Tenta realizar o parse de uma string com um json num objeto
 *
 * @param json Valor a ser convertido em objeto
 *
 * @return Objeto convertido ou false caso não seja um json válido
 */
export function tryParseJSON(json: string): any | false {
  try {
    return JSON.parse(json);
  } catch {
    return false;
  }
}

/**
 * @description Imprime no console todos os erros de validação de um form
 * @param form form a ser validade
 */
export function logFormValidationErros(form: FormGroup) {
  Object.keys(form.controls).forEach(key => {
    const controlErrors: ValidationErrors = form.get(key).errors;
    if (controlErrors != null) {
      Object.keys(controlErrors).forEach(keyError => {
        console.log('Key control: ' + key + ', keyError: ' + keyError + ', err value: ', controlErrors[keyError]);
      });
    }
  });
}

/**
 * @description Executa um callback na próxima rodada do event lookp
 * @param callback Função a ser executada na próxima rodada do event loop
 */
export function runOnNexEventLoop(callback: Function) {
  setTimeout(() => {
    callback();
  }, 0);
}
