import { Injectable, inject } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { firstValueFrom } from 'rxjs';
import type { RegistrarDespachoRequest } from '../models/despacho.model';
import { DespachoApi } from './despacho-api';

@Injectable({ providedIn: 'root' })
export class DespachoStore {
  private readonly api = inject(DespachoApi);

  readonly repuestos = rxResource({
    stream: () => this.api.getRepuestos(),
  });

  readonly historial = rxResource({
    stream: () => this.api.getHistorial(),
  });

  async registrar(request: RegistrarDespachoRequest) {
    const despachoCreado = await firstValueFrom(this.api.registrarDespacho(request));
    this.repuestos.reload();
    this.historial.reload();

    return despachoCreado;
  }
}
