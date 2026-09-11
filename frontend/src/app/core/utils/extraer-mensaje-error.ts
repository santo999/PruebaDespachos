import { HttpErrorResponse } from '@angular/common/http';
import type { ProblemDetails } from '../models/problem-details.model';

export function extraerMensajeError(error: unknown): string {
  if (error instanceof HttpErrorResponse) {
    const problema = error.error as ProblemDetails | undefined;

    if (problema?.errors) {
      const mensajes = Object.values(problema.errors).flat();
      if (mensajes.length > 0) {
        return mensajes.join(' ');
      }
    }

    if (problema?.detail) return problema.detail;
    if (problema?.title) return problema.title;
    if (error.status === 0) {
      return 'No se pudo conectar con el servidor. Verifica tu conexión.';
    }
  }

  return 'Ocurrió un error inesperado.';
}
