import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FrontService } from '../../services/front.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'smc-footer',
  templateUrl: './footer.component.html',
  providers: [ FrontService ],
  standalone: false,
})
export class FooterComponent implements OnInit {
  content: any;
  @Output() carregado = new EventEmitter();
  constructor(private service: FrontService,
    private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.content = "Carregando...";
    this.service.carregarConteudo("Front/PartialFooter").subscribe((result) => {
      this.content = this.sanitizer.bypassSecurityTrustHtml(result);
      this.carregado.emit();
    });
  }
}
