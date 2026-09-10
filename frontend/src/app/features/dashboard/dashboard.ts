import { ChangeDetectionStrategy, Component } from '@angular/core';
import { InventarioList } from '../inventario/inventario-list/inventario-list';
import { DespachoForm } from '../despachos/despacho-form/despacho-form';
import { DespachoHistorial } from '../despachos/despacho-historial/despacho-historial';

@Component({
  selector: 'app-dashboard',
  imports: [InventarioList, DespachoForm, DespachoHistorial],
  templateUrl: './dashboard.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class Dashboard {}
