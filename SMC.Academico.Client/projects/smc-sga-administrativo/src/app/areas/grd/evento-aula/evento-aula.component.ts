import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router, ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from 'projects/shared/utils/util';

@Component({
  selector: 'sga-evento-aula',
  templateUrl: './evento-aula.component.html',
  standalone: false,
})
export class EventoAulaComponent implements OnInit {
  addAgendamento: boolean = false;
  get origemTurma(): boolean {
    return this.route.snapshot.queryParams.origem === 'Turma';
  }

  constructor(private router: Router, private route: ActivatedRoute, private titleService: Title) {}

  ngOnInit(): void {
    this.titleService.setTitle('SGA.Administrativo - Agendamento de Aula');
  }

  exibirGrade(seqTurma: string) {
    if (isNullOrEmpty(seqTurma)) {
      this.router.navigate(['Index'], { relativeTo: this.route });
    } else {
      this.router.navigate(['Turma', seqTurma], { relativeTo: this.route, queryParamsHandling: 'preserve' });
    }
  }

  abrirModalAgendamento() {
    this.router.navigate([{ outlets: { modais: ['Add'] } }], { queryParamsHandling: 'preserve' });
  }

  voltar() {
    window.open('../TUR/Turma', '_self');
  }
}
