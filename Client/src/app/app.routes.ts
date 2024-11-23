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

import { VoucherComponent } from './component/admin/voucher/voucher.component';
import { AddVoucherComponent } from './component/admin/voucher/add-voucher/add-voucher.component';
import { HomeComponent } from './component/home/home.component';
import { ProductUserComponent } from './component/client/product/product.component';
import { ProductListComponent } from './component/client/productList/productList.component';
// import { NotFoundComponent } from './component/errors/not-found/not-found.component';
import { ServerErrorComponent } from './component/errors/server-error/server-error.component';
import { CategoryComponent } from './component/admin/category/category.component';
import { ProductFormComponent } from './component/admin/product/product-form/product-form.component';
import { BlogComponent } from './component/admin/blog/blog.component';
// import { AddBlogComponent } from './component/admin/blog/add-blog/add-blog.component';
import { BlogUserComponent } from './component/blog-user/blog-user.component';
import { CheckOutComponent } from './component/check-out/check-out.component';
import { ShippingMethodComponent } from './component/admin/shippingmethod/shippingmethod.component';
import { DiscountComponent } from './component/admin/discount/discount.component';
import { ProductDetailComponent } from './component/client/productDetail/productDetail.components';
export const routes: Routes = [
  { path: '', component: ProductUserComponent },
  { path: 'product/productList/:id', component: ProductListComponent },
  {path :'product/productDetail/:id' ,component : ProductDetailComponent},
  {
    path: 'admin',
    component: AdminComponent,
    children: [
      { path: '', component: DashboardComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'category', component: CategoryComponent },
      { path: 'product', component: ProductComponent },
      { path: 'product/add', component: ProductFormComponent },
      { path: 'product/edit/:id', component: ProductFormComponent },
      { path: 'product', component: ProductComponent },
      { path: 'voucher', component: VoucherComponent },
      { path: 'voucher/new', component: AddVoucherComponent },
      { path: 'voucher/edit/:id', component: AddVoucherComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'role', component: RoleComponent },
      { path: 'role/permission/:id', component: RolePermissionComponent },
      { path: 'blog', component: BlogComponent },
      { path: 'blog/new', component: AddBlogComponent },
      { path: 'blog/edit/:id', component: AddBlogComponent },
      { path: 'shippingmethod',component:ShippingMethodComponent},
      { path: 'discount' , component :DiscountComponent},
      // { path: 'blog/new', component: AddBlogComponent },
      // { path: 'blog/edit/:id', component: AddBlogComponent },
    ],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotpasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'home', component: HomeComponent },
  { path: 'blog/:id', component: BlogUserComponent },
  { path: 'checkout', component: CheckOutComponent },
  // { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
];
