import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PoUiModule } from './po-ui.module';
import { SmcAssertComponent } from 'projects/shared/components/smc-assert/smc-assert.component';
import { SmcButtonModule } from 'projects/shared/components/smc-button/smc-button.module';
import { SmcModalModule } from 'projects/shared/components/smc-modal/smc-modal.module';

@NgModule({
  declarations: [SmcAssertComponent],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    PoUiModule,
    SmcButtonModule,
    SmcModalModule,
  ],
  exports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    PoUiModule,
    SmcAssertComponent,
    SmcButtonModule,
    SmcModalModule,
  ],
})
export class SharedModule {}
