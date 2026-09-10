import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import type { Despacho, RegistrarDespachoRequest } from '../models/despacho.model';
import type { Repuesto } from '../models/repuesto.model';

@Injectable({ providedIn: 'root' })
export class DespachoApi {
  private readonly http = inject(HttpClient);

  private readonly baseUrl = environment.apiBaseUrl;

  getRepuestos(): Observable<Repuesto[]> {
    return this.http.get<Repuesto[]>(`${this.baseUrl}/repuestos`);
  }

  getHistorial(): Observable<Despacho[]> {
    return this.http.get<Despacho[]>(`${this.baseUrl}/despachos`);
  }

  registrarDespacho(request: RegistrarDespachoRequest): Observable<Despacho> {
    return this.http.post<Despacho>(`${this.baseUrl}/despachos`, request);
  }
}
