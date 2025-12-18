import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PoUiModule } from './po-ui.module';
import { SmcAssertComponent } from 'projects/shared/components/smc-assert/smc-assert.component';
import { SmcButtonModule } from 'projects/shared/components/smc-button/smc-button.module';
import { SmcModalModule } from 'projects/shared/components/smc-modal/smc-modal.module';
import { SmcTableModule } from 'projects/shared/components/smc-table/smc-table.module';

@NgModule({
  declarations: [SmcAssertComponent],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    PoUiModule,
    SmcButtonModule,
    SmcModalModule,
    SmcTableModule,
  ],
  exports: [
    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    PoUiModule,
    SmcAssertComponent,
    SmcButtonModule,
    SmcModalModule,
    SmcTableModule,
  ],
})
export class SharedModule {}
