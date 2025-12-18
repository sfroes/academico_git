import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApuracaoFrequenciaDiaModel } from '../models/apuracao-frequencia-dia.model';
import { ApuracaoFrequenciaDataService } from '../services/apuracao-frequencia-data.service';

@Component({
  selector: 'sga-apuracao-frequencia-grid-header',
  templateUrl: './apuracao-frequencia-grid-header.component.html',
  standalone: false,
  styles: [],
})
export class ApuracaoFrequenciaGridHeaderComponent implements OnInit {
  get dias$(): Observable<ApuracaoFrequenciaDiaModel[]> {
    return this.dataService.model$.pipe(map(m => m.dias.filter(f => f.mostrar)));
  }

  constructor(private dataService: ApuracaoFrequenciaDataService) {}

  ngOnInit(): void {}
}
