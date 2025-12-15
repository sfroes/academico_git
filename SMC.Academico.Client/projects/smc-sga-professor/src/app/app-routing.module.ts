import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LancamentoNotaComponent } from './areas/apr/lancamento-nota/lancamento-nota.component';
import { HomeComponent } from './shared/home/home.component';
import { LancamentoNotaResolverService } from './areas/apr/lancamento-nota/services/lancamento-nota-resolver.service';
import { ApuracaoFrequenciaComponent } from "./areas/apr/apuracao-frequencia-grade/apuracao-frequencia.component";

const routes: Routes = [
  {
    path: 'APR/LancamentoNota',
    component: LancamentoNotaComponent,
    resolve: { model: LancamentoNotaResolverService },
  },
  {
    path: 'APR/ApuracaoFrequenciaGrade/:seqOrigemAvaliacao',
    component: ApuracaoFrequenciaComponent,
  },
  { path: '**', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
