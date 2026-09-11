import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { extraerMensajeError } from '../../../core/utils/extraer-mensaje-error';
import { DespachoStore } from '../../../core/services/despacho-store';
import {
  AlertMessage,
  type TipoAlerta,
} from '../../../shared/components/alert-message/alert-message';

interface ResultadoOperacion {
  tipo: TipoAlerta;
  mensaje: string;
}
@Component({
  selector: 'app-despacho-form',
  imports: [ReactiveFormsModule, AlertMessage],
  templateUrl: './despacho-form.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DespachoForm {
  protected readonly store = inject(DespachoStore);
  private readonly fb = inject(FormBuilder);

  protected readonly form = this.fb.group({
    referenciaExterna: ['', [Validators.required]],
    repuestoId: ['', [Validators.required]],
    cantidad: [null as number | null, [Validators.required, Validators.min(1)]],
  });

  protected readonly enviando = signal(false);
  protected readonly resultado = signal<ResultadoOperacion | null>(null);

  protected async onSubmit(): Promise<void> {
    if (this.form.invalid || this.enviando()) return;

    this.enviando.set(true);
    this.resultado.set(null);

    try {
      const { referenciaExterna, repuestoId, cantidad } = this.form.getRawValue();
      const despachoCreado = await this.store.registrar({
        referenciaExterna: referenciaExterna!,
        repuestoId: repuestoId!,
        cantidad: cantidad!,
      });

      this.resultado.set({
        tipo: 'success',
        mensaje: `Despacho registrado correctamente para la referencia "${despachoCreado.referenciaExterna}".`,
      });

      this.form.reset();
    } catch (error) {
      this.resultado.set({ tipo: 'error', mensaje: extraerMensajeError(error) });
    } finally {
      this.enviando.set(false);
    }
  }
}
