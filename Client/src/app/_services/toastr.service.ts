import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root',
})
export class ToastrService {
  constructor(private messageService: MessageService) {}

  success(detail: string) {
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: detail,
      life: 3000,
    });
  }

  error(detail: string) {
    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: detail,
      life: 3000,
    });
  }

  info(detail: string) {
    this.messageService.add({
      severity: 'info',
      summary: 'Info',
      detail: detail,
      life: 3000,
    });
  }

  warn(detail: string) {
    this.messageService.add({
      severity: 'warn',
      summary: 'Warn',
      detail: detail,
      life: 3000,
    });
  }

  clear() {
    this.messageService.clear();
  }
}
