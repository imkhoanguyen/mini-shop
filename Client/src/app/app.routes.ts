import { Routes } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { CategoryComponent } from './component/admin/category/category.component';
import { ChatComponent } from './component/admin/chat/chat.component';
import { RoleComponent } from './component/admin/role/role.component';
import { AdminComponent } from './component/admin/admin.component';
import { DashboardComponent } from './component/admin/dashboard/dashboard.component';
import { ProductComponent } from './component/admin/product/product.component';
import { LoginComponent } from './component/login/login.component';
import { RegisterComponent } from './component/register/register.component';
import { OrderComponent } from './component/admin/order/order.component';
import { CourterComponent } from './component/admin/courter/courter.component';
import { RolePermissionComponent } from './component/admin/role-permission/role-permission.component';
import { ForgotpasswordComponent } from './component/forgotpassword/forgotpassword.component';
import { ResetPasswordComponent } from './component/reset-password/reset-password.component';
import { ProductFormComponent } from './component/admin/product/product-form/product-form.component';


export const routes: Routes = [
  { path: '', component: HeaderComponent },
  {
    path: 'admin',
    component: AdminComponent,
    children: [
      { path: '', component: DashboardComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'category', component: CategoryComponent },
      { path: 'product', component: ProductComponent},
      { path: 'product/add', component: ProductFormComponent },
      { path: 'product/edit/:id', component: ProductFormComponent},
      { path: 'chat', component: ChatComponent },
      { path: 'role', component: RoleComponent },
      { path: 'order',component: OrderComponent},
      { path: 'courter',component:CourterComponent},
      { path: 'role/permission/:id', component: RolePermissionComponent }
    ],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotpasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
];
