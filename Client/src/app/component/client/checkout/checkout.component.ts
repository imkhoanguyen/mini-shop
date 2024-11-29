import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../../_services/cart.service';
import { AddressService } from '../../../_services/address.service';
import { Address } from '../../../_models/address.module';
import { AccountService } from '../../../_services/account.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ShippingMethodService } from '../../../_services/shippingMethod.service';
import { ShippingMethodDto } from '../../../_models/shippingMethod.module';
import { DiscountService } from '../../../_services/discount.service';
import { DiscountDto } from '../../../_models/discount.module';

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
  private shippingMethodService = inject(ShippingMethodService);
  private discountService = inject(DiscountService);

  addressList: Address[] = [];
  shippingMethodList: ShippingMethodDto[] = [];
  discountCode = '';
  order: any = {
    description: '',
    shippingFee: 0,
    address: '',
    fullName: '',
    phoneNumber: '',
    discountId: 0,
    discountPrice: 0,
  };

  selectedAddress = 0;
  currentDiscount!: DiscountDto | null;

  ngOnInit(): void {
    if (!this.accountService.currentUser()) {
      this.router.navigateByUrl('/login');
    }
    this.loadAddress();
    this.loadShippingMethod();
    console.log(this.shippingMethodList[0].cost);
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

  loadShippingMethod() {
    this.shippingMethodService.getAllShippingMethods().subscribe({
      next: (res) => {
        this.shippingMethodList = res;
        this.order.shippingFee = res[0].cost;
      },
      error: (er) => {
        console.log(er);
      },
    });
  }

  onChangeAddress() {
    const address = this.addressList.find((a) => a.id == this.selectedAddress);
    this.order.address =
      address?.street + ' - ' + address?.district + ' - ' + address?.city;

    this.order.fullName = address?.fullName;
    this.order.phoneNumber = address?.phone;
  }

  onShippingMethodChange(item: ShippingMethodDto) {
    console.log('selected shopping method', item);
    this.order.shippingFee = item.cost;
  }

  onPaymentMethodChange(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    console.log('Selected payment method:', value);
  }

  applyDiscount() {
    this.discountService.getDiscountByCode(this.discountCode).subscribe({
      next: (res) => {
        this.currentDiscount = res.body;
      },
      error: (er) => console.log(er),
    });
    console.log(this.discountCode);
  }
}
