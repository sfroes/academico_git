import { CommonModule, registerLocaleData } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import localePtExtra from '@angular/common/locales/extra/pt';
import localePt from '@angular/common/locales/pt';
import { LOCALE_ID, NgModule } from '@angular/core';
import { PoNotificationService } from '@po-ui/ng-components';
import { AuthenticationInterceptor } from 'projects/shared/interceptors/authentication/authentication.interceptor';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { MiniProfilerInterceptor } from 'projects/shared/interceptors/mini-profiler/mini-profiler.interceptor';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { SessionErrorInterceptor } from 'projects/shared/interceptors/session-error/session-error.interceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AprModule } from './areas/apr/apr.module';
import { FooterComponent } from './shared/components/footer/footer.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { MenuComponent } from './shared/components/menu/menu.component';
import { HomeModule } from './shared/home/home.module';
import { SharedModule } from './shared/shared.module';
import { ErrorInterceptor } from 'projects/shared/interceptors/error/error.interceptor';

registerLocaleData(localePt, 'pt-BR', localePtExtra);

@NgModule({
  declarations: [AppComponent, HeaderComponent, MenuComponent, FooterComponent],
  imports: [CommonModule, AppRoutingModule, SharedModule, AprModule, HomeModule],
  bootstrap: [AppComponent],
  providers: [
    SmcNotificationService,
    SmcLoadService,
    PoNotificationService,
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: SessionErrorInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MiniProfilerInterceptor,
      multi: true,
    },
  ],
})
export class AppModule { }
