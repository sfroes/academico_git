import { DOCUMENT } from '@angular/common';
import { ChangeDetectorRef, Component, ElementRef, Inject, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { AppConstants } from './app.constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
})
export class AppComponent implements OnInit {
  componentesCarregados: number = 0;
  carregando = false;

  constructor(private elementRef: ElementRef,
    private title: Title,
    private loadingService: SmcLoadService,
    private changeDetection: ChangeDetectorRef,
    @Inject(DOCUMENT) private document: Document) { }

  ngOnInit() {
    this.title.setTitle(AppConstants.TITLE);
    this.loadingService.isLoading.subscribe(isLoading => {
      this.carregando = isLoading;
      this.changeDetection.detectChanges();
    });
  }

  componenteCarregado() {
    this.componentesCarregados++;
    if (this.componentesCarregados >= 3) {
      this.initMenu();
    }
  }

  initMenu() {
    var m = this.document.createElement("script");
    m.type = "text/javascript";
    m.src = "/Recursos/4.0/Scripts/bundles/smc-base.js";
    this.elementRef.nativeElement.appendChild(m);
  }
}
