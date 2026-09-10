import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { DespachoStore } from '../../../core/services/despacho-store';

@Component({
  selector: 'app-inventario-list',
  imports: [],
  templateUrl: './inventario-list.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InventarioList {
  protected readonly store = inject(DespachoStore);
}
