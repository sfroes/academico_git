import { DOCUMENT } from '@angular/common';
import { Component, EventEmitter, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { SmcButtonComponent } from 'projects/shared/components/smc-button/smc-button.component';
import { SmcModalComponent } from 'projects/shared/components/smc-modal/smc-modal.component';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { Subscription } from 'rxjs';
import { ApuracaoFrequenciaAulaModel } from './models/apuracao-frequencia-aula.model';
import { ApuracaoFrequenciaDataService } from './services/apuracao-frequencia-data.service';
import { ApuracaoFrequenciaService } from './services/apuracao-frequencia.service';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'sga-apuracao-frequencia',
  templateUrl: './apuracao-frequencia.component.html',
  standalone: false,
})
export class ApuracaoFrequenciaComponent implements OnInit, OnDestroy {
  maximizar = false;
  existemItensAlterados = false;
  qtdAulasEmApuracao = 0;
  qtdAulasEmCancelamento = 0;
  dadosCarregados = true;
  get descricaoComponenteSafeHtml() {
    return this.sanitizer.bypassSecurityTrustHtml(this.dataService.modelSnapshot.descricaoOrigemAvaliacao);
  }
  aluasMensagemSalvar: string[] = [];
  get form() {
    return this.dataService.form;
  }
  get processando$() {
    return this.dataService.processando$;
  }
  @ViewChild('btnAssertVoltarNao') btnAssertVoltarNao: SmcButtonComponent;
  @ViewChild('btnAssertSalvarNao') btnAssertSalvarNao: SmcButtonComponent;
  private _subs: Subscription[] = [];
  private set subs(value: Subscription) {
    this._subs.push(value);
  }

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private activatedRoute: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private service: ApuracaoFrequenciaService,
    public dataService: ApuracaoFrequenciaDataService,
    private notificationService: SmcNotificationService
  ) {}

  ngOnInit(): void {
    this.subs = this.activatedRoute.params.subscribe(params => {
      this.service.carregarDados(params['seqOrigemAvaliacao']);
    });

    this.subs = this.dataService.emApuracao$.subscribe(s => {
      this.qtdAulasEmApuracao = s;
      this.existemItensAlterados = this.qtdAulasEmApuracao > 0 || this.qtdAulasEmCancelamento > 0;
    });

    this.subs = this.dataService.emCancelamento$.subscribe(s => {
      this.qtdAulasEmCancelamento = s;
      this.existemItensAlterados = this.qtdAulasEmApuracao > 0 || this.qtdAulasEmCancelamento > 0;
    });

    this.subs = this.dataService.model$.subscribe(s => {
      this.dadosCarregados = s.descricaoOrigemAvaliacao === 'Carregando...';
    });
  }

  ngOnDestroy(): void {
    this._subs.forEach(f => f.unsubscribe());
  }

  validarAssertSalvar(confirmar: SmcModalComponent) {
    this.aluasMensagemSalvar = [];
    this.validarAulasPreenchidas();

    if (this.aluasMensagemSalvar.length === 0) {
      return this.onSubmit();
    } else {
      confirmar.open();
      this.btnAssertSalvarNao.focus();
    }
  }

  async onSubmit(confirmar?: SmcModalComponent) {
    let sucesso = true;
    confirmar?.close();
    this.processando$.next(true);
    const seqOrigemAvaliacao = this.activatedRoute.snapshot.params['seqOrigemAvaliacao'] as string;
    this.notificationService.information(`Iniciando a gravação do lançamento da frequencia.`);
    try {
      await this.service.salvarDados();
    } catch {
      sucesso = false;
    }
    this.form.markAsPristine();
    await this.service.carregarDados(seqOrigemAvaliacao);
    if (sucesso) {
      this.notificationService.success('Gravação do lançamento da frequência. Concluída com sucesso.');
    }
    this.dataService.emApuracao$.next(0);
    this.dataService.emCancelamento$.next(0);
    this.processando$.next(false);
  }

  validarAsserVoltar(confirmar: SmcModalComponent) {
    if (this.maximizar) {
      this.maximizar = !this.maximizar;
    } else {
      if (
        this.dataService.model$.value.aulas.filter(f => f.situacaoApuracaoFrequencia === 'Em apuração').length === 0
      ) {
        return this.voltar();
      } else {
        confirmar.open();
        this.btnAssertVoltarNao.focus();
      }
    }
  }

  voltar() {
    this.document.location.href = '../';
  }

  private validarAulasPreenchidas() {
    const aulasEmApuracao = this.dataService.modelSnapshot.aulas.filter(
      f => f.situacaoApuracaoFrequencia === 'Em apuração'
    );
    aulasEmApuracao.forEach(aula => {
      const apuracoes = this.form.value.alunos
        .flatMap(f => f.apuracoes)
        .filter(f => f.seqEventoAula === aula.seqEventoAula);
      const ausente = apuracoes.filter(f => f.frequencia === 'Ausente').length;
      const presente = apuracoes.filter(f => f.frequencia !== 'Ausente').length;

      this.aluasMensagemSalvar.push(`${aula.sigla}: ${ausente} aluno(s) ausente(s) e ${presente} aluno(s) presente(s)`);
    });
  }

  clickMaximizar(event) {
    this.maximizar = event;
  }
}
