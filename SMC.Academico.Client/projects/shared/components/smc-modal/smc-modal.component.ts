import { Component, ElementRef, ViewChild } from '@angular/core';

import { v4 as uuid } from 'uuid';

import { SmcModalBaseComponent } from './smc-modal-base.component';
import { SmcModalService } from './smc-modal-service';

/**
 * @docsExtends SmcModalBaseComponent
 */

@Component({
  selector: 'smc-modal',
  templateUrl: './smc-modal.component.html',
  standalone: false,
})
export class SmcModalComponent extends SmcModalBaseComponent {
  @ViewChild('modalContent', { read: ElementRef }) modalContent: ElementRef;

  private firstElement;
  private focusFunction;
  private focusableElements = 'input, select, textarea, button:not([disabled]), a';
  private id: string = uuid();
  private sourceElement;

  onResizeListener: () => void;

  constructor(private smcModalService: SmcModalService) {
    super();
  }

  close(xClosed = false) {
    this.smcModalService.modalActive = undefined;

    super.close(xClosed);

    this.removeEventListeners();

    if (this.sourceElement) {
      this.sourceElement.focus();
    }
  }

  closeModalOnEscapeKey(event) {
    if (!this.hideClose) {
      event.preventDefault();
      event.stopPropagation();
      this.close();
    }
  }

  onClickOut(event) {
    if (this.clickOut && !this.modalContent.nativeElement.contains(event.target)) {
      this.close();
    }
  }

  open() {
    this.sourceElement = document.activeElement;

    super.open();

    this.handleFocus();
  }

  private handleFocus(): any {
    this.smcModalService.modalActive = this.id;

    setTimeout(() => {
      if (this.modalContent) {
        this.initFocus();
        document.addEventListener('focus', this.focusFunction, true);
      }
    });
  }

  private initFocus() {
    this.focusFunction = (event: any) => {
      this.smcModalService.modalActive = this.smcModalService.modalActive || this.id;
      const modalElement = this.modalContent.nativeElement;

      if (!modalElement.contains(event.target) && this.smcModalService.modalActive === this.id) {
        event.stopPropagation();
        this.firstElement.focus();
      }
    };

    this.setFirstElement();

    if (this.hideClose) {
      this.firstElement.focus();
    } else {
      const firstFieldElement =
        this.modalContent.nativeElement.querySelectorAll(this.focusableElements)[1] || this.modalContent.nativeElement;
      firstFieldElement.focus();
    }
  }

  private removeEventListeners() {
    document.removeEventListener('focus', this.focusFunction, true);
  }

  private setFirstElement() {
    this.firstElement =
      this.modalContent.nativeElement.querySelector(this.focusableElements) || this.modalContent.nativeElement;
  }
}
