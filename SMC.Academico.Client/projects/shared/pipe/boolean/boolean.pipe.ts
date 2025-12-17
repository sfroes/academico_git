import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'boolean',
  standalone: false,
})
export class BooleanPipe implements PipeTransform {
  /**
   * #### Description
   *  Boleanos em sim e não
   * Transforms boolean pipe
   * @param value
   * @returns transform
   */
  transform(value): string {
    switch (value) {
      case false:
        return 'Não';
      case true:
        return 'Sim';
      default:
        return '';
    }
  }
}
