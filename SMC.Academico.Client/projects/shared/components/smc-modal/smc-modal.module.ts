import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SmcModalComponent } from './smc-modal.component';
import { SmcModalService } from './smc-modal-service';

@NgModule({
  declarations: [SmcModalComponent],
  imports: [
    CommonModule
  ],
  exports: [SmcModalComponent],
  providers: [SmcModalService]
})
export class SmcModalModule { }
