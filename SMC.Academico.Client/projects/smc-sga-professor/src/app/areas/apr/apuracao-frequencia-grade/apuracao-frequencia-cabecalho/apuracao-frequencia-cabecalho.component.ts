import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { map } from 'rxjs/operators';
import { ApuracaoFrequenciaDataService } from '../services/apuracao-frequencia-data.service';

@Component({
  selector: 'sga-apuracao-frequencia-cabecalho',
  templateUrl: './apuracao-frequencia-cabecalho.component.html',
})
export class ApuracaoFrequenciaCabecalhoComponent implements OnInit {
  get descricaoOrigemAvaliacao$() {
    return this.dataService.model$.pipe(map(m => this.sanitizer.bypassSecurityTrustHtml(m.descricaoOrigemAvaliacao)));
  }
  constructor(private dataService: ApuracaoFrequenciaDataService, private sanitizer: DomSanitizer) {}

  ngOnInit(): void {}
}
