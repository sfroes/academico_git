import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BooleanPipe } from './boolean/boolean.pipe';
import { SafeHtmlPipe } from './safeHtml/safeHtml.pipe';

@NgModule({
  declarations: [BooleanPipe, SafeHtmlPipe],
  imports: [CommonModule],
  exports: [BooleanPipe, SafeHtmlPipe],
})
export class PipeModule {}
