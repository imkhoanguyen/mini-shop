import { Routes } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { ChatComponent } from './component/admin/chat/chat.component';
import { RoleComponent } from './component/admin/role/role.component';
import { AdminComponent } from './component/admin/admin.component';
import { DashboardComponent } from './component/admin/dashboard/dashboard.component';
import { ProductComponent } from './component/admin/product/product.component';
import { LoginComponent } from './component/login/login.component';
import { RegisterComponent } from './component/register/register.component';
import { RolePermissionComponent } from './component/admin/role-permission/role-permission.component';
import { ForgotpasswordComponent } from './component/forgotpassword/forgotpassword.component';
import { ResetPasswordComponent } from './component/reset-password/reset-password.component';

import { HomeComponent } from './component/home/home.component';
// import { NotFoundComponent } from './component/errors/not-found/not-found.component';
import { ServerErrorComponent } from './component/errors/server-error/server-error.component';
import { CategoryComponent } from './component/admin/category/category.component';
import { ProductFormComponent } from './component/admin/product/product-form/product-form.component';
import { BlogComponent } from './component/admin/blog/blog.component';
// import { AddBlogComponent } from './component/admin/blog/add-blog/add-blog.component';
import { BlogUserComponent } from './component/blog-user/blog-user.component';
import { authGuard } from './_guards/auth.guard';
import { ShopComponent } from './component/client/shop/shop.component';
import { UserComponent } from './component/admin/user/user.component';
import { CartComponent } from './component/client/cart/cart.component';
import { StatisticComponent } from './component/admin/statistic/statistic.component';
import { CheckoutComponent } from './component/client/checkout/checkout.component';
import { ShippingMethodComponent } from './component/admin/shippingmethod/shippingmethod.component';
import { DiscountComponent } from './component/admin/discount/discount.component';
import { UserProfileComponent } from './component/client/user-profile/user-profile.component';
import { CheckoutSuccessComponent } from './component/client/checkout-success/checkout-success.component';
import { CheckoutCancelComponent } from './component/client/checkout-cancel/checkout-cancel.component';
import { ProductDetailComponent } from './component/client/product-detail/product-detail.component';
import { OrderComponent } from './component/admin/order/order.component';
import { OrderDetailComponent } from './component/admin/order-detail/order-detail.component';
import { AddBlogComponent } from './component/admin/blog/add-blog/add-blog.component';
export const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [authGuard],
    children: [
      { path: '', component: DashboardComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'category', component: CategoryComponent },
      { path: 'product', component: ProductComponent },
      { path: 'product/add', component: ProductFormComponent },
      { path: 'product/edit/:id', component: ProductFormComponent },
      { path: 'product', component: ProductComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'role', component: RoleComponent },
      { path: 'role/permission/:id', component: RolePermissionComponent },
      { path: 'blog', component: BlogComponent },
      { path: 'user', component: UserComponent },
      { path: 'statistic', component: StatisticComponent },
      { path: 'shippingmethod', component: ShippingMethodComponent },
      { path: 'discount', component: DiscountComponent },
      { path: 'order', component: OrderComponent },
      { path: 'order/:id', component: OrderDetailComponent },
      { path: 'blog/new', component: AddBlogComponent },
      { path: 'blog/edit/:id', component: AddBlogComponent },
    ],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotpasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'home', component: HomeComponent },
  { path: 'blog/:id', component: BlogUserComponent },
  { path: 'cart/checkout', component: CheckoutComponent },
  // { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: 'shop', component: ShopComponent },
  { path: 'shop/product-detail/:productId', component: ProductDetailComponent },
  { path: 'cart', component: CartComponent },
  { path: 'user-profile', component: UserProfileComponent },
  {
    path: 'cart/checkout/success',
    component: CheckoutSuccessComponent,
  },
  { path: 'cart/checkout/cancel', component: CheckoutCancelComponent },
];
