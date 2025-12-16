import { AbstractControl, ValidatorFn } from '@angular/forms';

export function smcMax(value: number): ValidatorFn {
  return (c: AbstractControl): { [key: string]: number } | null => {
    const inputValue = +(c.value as string)?.replace(',', '.');
    if (!Number.isNaN(inputValue) && inputValue > value) {
      return { smcMax: inputValue };
    }
    return null;
  };
}

export function conditionalValidator(condition: () => boolean, validator: ValidatorFn): ValidatorFn {
  return (c: AbstractControl): { [key: string]: boolean } | null => {
    return condition() ? validator(c) : null;
  };
}
