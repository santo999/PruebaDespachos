export interface Despacho {
  id: string;
  referenciaExterna: string;
  repuestoId: string;
  repuestoSku: string;
  repuestoNombre: string;
  cantidad: number;
  fechaRegistro: string;
}

export interface RegistrarDespachoRequest {
  referenciaExterna: string;
  repuestoId: string;
  cantidad: number;
}
