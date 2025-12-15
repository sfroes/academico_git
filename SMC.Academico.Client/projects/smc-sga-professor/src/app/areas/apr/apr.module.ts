import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { LancamentoNotaModule } from "./lancamento-nota/lancamento-nota.module";
import { ApuracaoFrequenciaGradeModule } from "./apuracao-frequencia-grade/apuracao-frequencia-grade.module";

@NgModule({
  declarations: [],
  imports: [CommonModule, LancamentoNotaModule, ApuracaoFrequenciaGradeModule],
  exports: [LancamentoNotaModule, ApuracaoFrequenciaGradeModule],
})
export class AprModule {}
