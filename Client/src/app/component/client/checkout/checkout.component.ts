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
import { OrderAdd } from '../../../_models/order';
import { UtilityService } from '../../../_services/utility.service';
import { OrderService } from '../../../_services/order.service';
import { ToastrService } from '../../../_services/toastr.service';
import { PaymentService } from '../../../_services/payment.service';

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
  private utilService = inject(UtilityService);
  private orderService = inject(OrderService);
  private toastrService = inject(ToastrService);
  private paymentService = inject(PaymentService);

  validationErrors?: string[];

  addressList: Address[] = [];
  shippingMethodList: ShippingMethodDto[] = [];
  discountCode = '';
  order: any = {
    description: '',
    shippingFee: 0,
    shippingMethodId: 0,
    address: '',
    fullName: '',
    phoneNumber: '',
    discountId: 0,
    discountPrice: 0,
  };

  selectedAddress = 0;
  currentDiscount!: DiscountDto | null;
  selectedPaymentMethod = 0;

  ngOnInit(): void {
    if (!this.accountService.currentUser()) {
      this.router.navigateByUrl('/login');
    }
    this.loadAddress();
    this.loadShippingMethod();
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
        this.order.shippingMethodId = res[0].id;
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
    this.order.phone = address?.phone;
  }

  onShippingMethodChange(item: ShippingMethodDto) {
    console.log('selected shopping method', item);
    this.order.shippingFee = item.cost;
    this.order.shippingMethodId = item.id;
  }

  onPaymentMethodChange(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.selectedPaymentMethod = +value;
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

  calDiscountPrice() {
    if (this.currentDiscount == null) return 0;

    let priceDiscount = 0;
    if (this.currentDiscount.amountOff !== 0) {
      priceDiscount = this.currentDiscount.amountOff;
    } else if (this.currentDiscount.percentOff !== 0) {
      priceDiscount =
        ((this.cartService.totals() + this.order.shippingFee) *
          this.currentDiscount.percentOff) /
        100;
    }

    return priceDiscount;
  }

  calTotal() {
    return (
      this.cartService.totals() +
      this.order.shippingFee -
      this.calDiscountPrice()
    );
  }

  onAddOrderOrPayment() {
    const orderAdd: OrderAdd = {
      address: this.order.address,
      phone: this.order.phone,
      fullName: this.order.fullName,
      shippingFee: this.order.shippingFee,
      shippingMethodId: this.order.shippingMethodId,

      description: this.order.description,
      subTotal: this.cartService.totals(),
      items: this.cartService.cart()?.items || [],
      paymentMethod:
        this.selectedPaymentMethod === 0
          ? this.utilService.PAYMENT_OFFILINE
          : this.utilService.PAYMENT_ONLINE,
    };

    if (this.currentDiscount) {
      orderAdd.discountId = this.currentDiscount?.id;
      orderAdd.discountPrice = this.calDiscountPrice();
    }

    if (this.selectedPaymentMethod === 0) {
      orderAdd.paymentMethod = this.utilService.PAYMENT_OFFILINE;

      this.orderService.addOrder(orderAdd).subscribe({
        next: (res) => {
          console.log(res);
          this.toastrService.success('Đặt hàng thành công');
          this.router.navigate(['cart/checkout/success'], {
            queryParams: {
              order_id: res.id,
            },
          });
        },
        error: (er) => {
          this.validationErrors = er;
          console.log(er);
        },
      });
    } else if (this.selectedPaymentMethod === 1) {
      orderAdd.paymentMethod = this.utilService.PAYMENT_ONLINE;
      this.paymentService
        .createSessionCheckout(orderAdd, this.cartService.cart()?.id ?? '')
        .subscribe({
          next: (res: any) => {
            window.location.href = res.url;
          },
          error: (er) => {
            this.validationErrors = er;
            console.log(er);
          },
        });
    }
  }
}
