import {
  Component,
  Output,
  EventEmitter,
  Input,
  OnChanges,
  SimpleChanges,
  OnInit,
  ChangeDetectorRef,
  ViewChildren,
  ElementRef,
  QueryList,
} from '@angular/core';
import { Subject } from 'rxjs';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { filter, debounceTime, tap } from 'rxjs/operators';

@Component({
  selector: 'smc-lookup-busca-simples',
  templateUrl: './smc-lookup-busca-simples.component.html',
  styles: [],
  standalone: false,
})
export class SmcLookupBuscaSimplesComponent implements OnInit, OnChanges {
  valueSelecioando: string;
  currentValue = ';';
  exibirResultado = false;
  loading = false;
  seqValueSelecionado: string;
  eventosInput = new Subject<string>();
  eventosOutput = new Subject<string>();
  currentIndex = -1;

  @Input('s-listaResultado') listaResultado: SmcKeyValueModel[] = [];
  @Input('s-label') label = 'Label';
  @Input('s-readonly') readonly = false;
  @Input('s-required') required = false;
  @Input('s-value') value: SmcKeyValueModel = null;
  @Output('s-respostaSelecionado') respostaSelecionado = new EventEmitter<string>();
  @Output('s-pesquisa') pesquisa = new EventEmitter<string>();
  @Output('s-touched') touched = new EventEmitter();
  @Output('s-buscaAvancada') buscaAvancada = new EventEmitter();

  constructor(private changeDectorRef: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.eventosInput
      .pipe(
        filter(f => f.length >= 3),
        tap(_ => {
          this.exibirResultado = true;
          this.loading = true;
        }),
        debounceTime(1500)
      )
      .subscribe(filtro => this.pesquisa.emit(filtro));

    // Permite ao Angular terminar o processamento do evento de input antes de emitir o evento de output
    // Equivalente ao setTimmeout(0) para adiar esse processamento para a próxima rodada do loop de eventos do Javascript
    this.eventosOutput.pipe(debounceTime(0)).subscribe(_ => this.respostaSelecionado.emit(this.seqValueSelecionado));

    this.atualizarValor(this.value);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['value']) {
      this.itemSelecionado(this.value, false);
    }

    if (changes['listaResultado']) {
      if (this.listaResultado.length === 1) {
        this.valueSelecioando = this.listaResultado[0].value;
        this.seqValueSelecionado = this.listaResultado[0].key;
        this.itemSelecionado(this.listaResultado[0]);
      }

      this.exibirResultado = this.listaResultado.length > 1;
      this.loading = false;
    }
  }

  limparPesquisa() {
    this.currentValue = '';
    this.valueSelecioando = '';
    this.seqValueSelecionado = '';
    this.exibirResultado = false;
    this.respostaSelecionado.emit('');
  }

  buscaDetalhada() {
    this.exibirResultado = true;
  }

  buscaSimples() {
    this.eventosInput.next(this.valueSelecioando);
  }

  itemSelecionado(item: SmcKeyValueModel, emit = true) {
    this.valueSelecioando = item?.value ?? '';
    this.currentValue = item?.value ?? '';
    this.seqValueSelecionado = item?.key ?? '';
    this.exibirResultado = false;
    this.listaResultado = [];
    if (emit) {
      this.eventosOutput.next(this.seqValueSelecionado);
    }
  }

  onBlur() {
    // Timeout para dar ao usuário tempo de selecionar um item na lista
    // Caso contrário no blur será "restaurado" o item anterior imediatamente
    setTimeout(() => {
      this.exibirResultado = false;
      if (this.currentValue !== this.valueSelecioando) {
        this.valueSelecioando = this.currentValue;
      }
    }, 500);
  }

  onFocus() {
    this.currentValue = this.valueSelecioando;
    this.valueSelecioando = '';
  }

  onKeyDown(event: KeyboardEvent) {
    if (!this.listaResultado?.length) {
      return;
    }
    if (event.key === 'ArrowDown') {
      this.currentIndex++;
      if (this.currentIndex >= this.listaResultado.length) {
        this.currentIndex = 0;
      }
      this.valueSelecioando = this.listaResultado[this.currentIndex].value;
    } else if (event.key === 'ArrowUp') {
      this.currentIndex--;
      if (this.currentIndex < 0) {
        this.currentIndex = this.listaResultado.length - 1;
      }
      this.valueSelecioando = this.listaResultado[this.currentIndex].value;
    } if (event.key.startsWith('Arrow')) {
      // Faz o comportamento padrão
      return;
    } else {
      this.currentIndex = -1;
      const result = this.listaResultado?.filter(f => f.value === this.valueSelecioando);
      if (result?.length === 1) {
        this.itemSelecionado(result[0]);
      }
    }
  }

  private atualizarValor(value: SmcKeyValueModel) {
    this.seqValueSelecionado = value?.key;
    this.valueSelecioando = value?.value;
  }
}
