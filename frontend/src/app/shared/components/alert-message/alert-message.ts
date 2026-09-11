import { Component, input } from '@angular/core';

export type TipoAlerta = 'success' | 'error';

@Component({
  selector: 'app-alert-message',
  imports: [],
  templateUrl: './alert-message.html',
})
export class AlertMessage {
  readonly tipo = input.required<TipoAlerta>();
  readonly mensaje = input.required<string>();
}
