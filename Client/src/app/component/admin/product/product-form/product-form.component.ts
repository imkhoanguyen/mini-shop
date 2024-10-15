import { Component } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { Subscription } from 'rxjs';
import { StepsModule } from 'primeng/steps';
@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [StepsModule, ToastModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.css',
  providers: [MessageService]
})
export class ProductFormComponent {
  items: MenuItem[] | undefined;
  subscription: Subscription | undefined;

  constructor(private messageService: MessageService) {}
  ngOnInit() {
    this.items = [
      {
        label: 'Sản phẩm',
        routerLink: 'addProduct'
      },
      {
        label: 'Biến thể',
        routerLink: 'addVariant'
      },

    ];
  }
  ngOnDestroy() {
    if (this.subscription) {
        this.subscription.unsubscribe();
    }
  }
}
