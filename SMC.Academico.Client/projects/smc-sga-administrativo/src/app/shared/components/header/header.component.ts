import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FrontService } from '../../services/front.service';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'smc-header',
  templateUrl: './header.component.html',
  providers: [ FrontService ]
})
export class HeaderComponent implements OnInit {
  content: any;
  @Output() carregado = new EventEmitter();
  constructor(private service: FrontService,
    private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.content = "Carregando...";
    this.service.carregarConteudo("Front/PartialHeader").subscribe(result => {
      this.content = this.sanitizer.bypassSecurityTrustHtml(result);
      this.carregado.emit();
    });
  }
}
