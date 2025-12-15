import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './shared/home/home.component';
import { SituacaoAulaLoteComponent } from './areas/apr/situacao-aula-lote/situacao-aula-lote.component';
import { GradeHorarioCompartilhadaComponent } from './areas/grd/grade-horario-compartilhada/grade-horario-compartilhada.component';

const routes: Routes = [
  { path: 'APR/SituacaoAulaLote', component: SituacaoAulaLoteComponent },
  { path: 'GRD/GradeHorarioCompartilhada', component: GradeHorarioCompartilhadaComponent },
  { path: '**', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
