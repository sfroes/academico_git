import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { PoContainerModule } from '@po-ui/ng-components';
import { PoFieldModule } from '@po-ui/ng-components';

import { SmcTreeViewComponent } from './smc-tree-view.component';
import { SmcTreeViewItemComponent } from './smc-tree-view-item/smc-tree-view-item.component';
import { SmcTreeViewItemHeaderComponent } from './smc-tree-view-item-header/smc-tree-view-item-header.component';

/**
 * @description
 *
 * Módulo do componente `smc-tree-view`.
 *
 * > Para o correto funcionamento do componente `smc-tree-view`, deve ser importado o módulo `BrowserAnimationsModule` no
 * > módulo principal da sua aplicação.
 *
 * Módulo da aplicação:
 * ```
 * import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
 * import { SmcModule } from '@po-ui/ng-components';
 * ...
 *
 * @NgModule({
 *   imports: [
 *     BrowserModule,
 *     BrowserAnimationsModule,
 *     ...
 *     PoModule
 *   ],
 *   declarations: [
 *     AppComponent,
 *     ...
 *   ],
 *   providers: [],
 *   bootstrap: [AppComponent]
 * })
 * export class AppModule { }
 * ```
 */
@NgModule({
  declarations: [SmcTreeViewComponent, SmcTreeViewItemComponent, SmcTreeViewItemHeaderComponent],
  exports: [SmcTreeViewComponent],
  imports: [CommonModule, FormsModule, PoContainerModule, PoFieldModule]
})
export class SmcTreeViewModule {}
