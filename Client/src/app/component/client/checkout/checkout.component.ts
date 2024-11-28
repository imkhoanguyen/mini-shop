import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../../_services/cart.service';
import { AddressService } from '../../../_services/address.service';
import { Address } from '../../../_models/address.module';
import { AccountService } from '../../../_services/account.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit {
  cartService = inject(CartService);
  private addressService = inject(AddressService);
  accountService = inject(AccountService);
  private router = inject(Router);
  addressList: Address[] = [];
  order: any = {
    address: '',
    description: '',
  };

  ngOnInit(): void {
    if (!this.accountService.currentUser()) {
      this.router.navigateByUrl('/login');
    }
    this.loadAddress();
  }

  loadAddress() {
    this.addressService
      .getAllAddressByUserId(this.accountService.currentUser()!.id)
      .subscribe({
        next: (res) => {
          this.addressList = res;
        },
        error: (er) => {
          console.log(er);
        },
      });
  }

  onChangeAddress() {
    console.log(this.order.address);
  }
}
