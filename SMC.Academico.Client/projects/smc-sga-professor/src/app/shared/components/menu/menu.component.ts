import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FrontService } from '../../services/front.service';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'smc-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.sass'],
  standalone: false,
  providers: [FrontService]
})
export class MenuComponent implements OnInit {
  content: any;
  @Output() carregado = new EventEmitter();

  constructor(private service: FrontService,
    private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.content = "Carregando...";
    this.service.carregarConteudo("Front/PartialMenu").subscribe(result => {
      this.content = this.sanitizer.bypassSecurityTrustHtml(result);
      this.carregado.emit();
    });
  }
}
