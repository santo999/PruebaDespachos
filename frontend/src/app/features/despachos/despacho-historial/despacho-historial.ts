import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { DespachoStore } from '../../../core/services/despacho-store';

@Component({
  selector: 'app-despacho-historial',
  imports: [DatePipe],
  templateUrl: './despacho-historial.html',
})
export class DespachoHistorial {
  protected readonly store = inject(DespachoStore);
}
