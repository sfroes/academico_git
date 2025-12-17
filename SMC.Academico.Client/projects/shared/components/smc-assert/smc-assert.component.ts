import { Component, ViewChild, Input } from '@angular/core';
import { PoModalAction, PoModalComponent } from '@po-ui/ng-components';

@Component({
  selector: 'app-smc-assert',
  templateUrl: './smc-assert.component.html',
  standalone: false,
})
export class SmcAssertComponent {
  @Input() title = '[title]';
  @Input() clickout = false;
  @Input() size: 'sm' | 'md' | 'lg' | 'xl' | 'auto' = 'auto';
  @Input() confirmCallback: () => void;
  @ViewChild(PoModalComponent, { static: true }) poModal: PoModalComponent;
  message: string;

  secondaryAction: PoModalAction = {
    action: () => {
      this.poModal.close();
      this.confirmCallback();
    },
    label: 'Sim',
  };

  primaryAction: PoModalAction = {
    action: () => {
      this.poModal.close();
    },
    label: 'Não',
  };

  showMessage(message?: string, title?: string): void {
    if (title) {
      this.title = title;
    }
    this.message = message;
    this.poModal.open();
  }
}
