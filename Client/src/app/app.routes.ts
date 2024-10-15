import { Routes } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { CategoryComponent } from './component/admin/category/category.component';
import { ChatComponent } from './component/admin/chat/chat.component';
import { RoleComponent } from './component/admin/role/role.component';
import { AdminComponent } from './component/admin/admin.component';
import { DashboardComponent } from './component/admin/dashboard/dashboard.component';
import { ProductComponent } from './component/admin/product/product.component';
import { ProductaddComponent } from './component/admin/product/productadd/productadd.component';
import { LoginComponent } from './component/login/login.component';
import { RegisterComponent } from './component/register/register.component';
import { RolePermissionComponent } from './component/admin/role-permission/role-permission.component';
import { ProductFormComponent } from './component/admin/product/product-form/product-form.component';
import { AddProductComponent } from './component/admin/product/add-product/add-product.component';
import { AddVariantComponent } from './component/admin/product/add-variant/add-variant.component';

export const routes: Routes = [
  { path: '', component: HeaderComponent },
  {
    path: 'admin',
    component: AdminComponent,
    children: [
      { path: '', component: DashboardComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'category', component: CategoryComponent },
      { path: 'product', component: ProductComponent },
      { path: 'productForm', component: ProductFormComponent, children: [
        {path: 'addProduct', component: AddProductComponent},
        {path: 'addVariant', component: AddVariantComponent}
      ] },
      { path: 'chat', component: ChatComponent },
      { path: 'role', component: RoleComponent },
      { path: 'role/permission/:id', component: RolePermissionComponent },
    ],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

];
