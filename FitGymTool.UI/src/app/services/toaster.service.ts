import { inject, Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

/**
 * The Toaster Service.
 */
@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  private readonly messageService: MessageService = inject(MessageService);

  /**
   * Handles the success message toaster event.
   * @param message The success message.
   */
  public showSuccess(message: string): void {
    this.messageService.add({
      key: 'confirm',
      severity: 'success',
      summary: 'Success',
      detail: message,
      life: 5000,
    });
  }

  /**
   * Handles the error message toaster event.
   * @param message The error messsage.
   */
  public showError(message: string): void {
    this.messageService.add({
      key: 'confirm',
      severity: 'error',
      summary: 'Error',
      detail: message,
      life: 10000,
    });
  }
}
