import { Routes } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { CategoryComponent } from './component/admin/category/category.component';
import { ChatComponent } from './component/admin/chat/chat.component';
import { RoleComponent } from './component/admin/role/role.component';
import { AdminComponent } from './component/admin/admin.component';
import { DashboardComponent } from './component/admin/dashboard/dashboard.component';
import { ProductComponent } from './component/admin/product/product.component';
import { ProductaddComponent } from './component/admin/product/productadd/productadd.component';
import { ProductUserComponent } from './component/client/product/product.component';
import { ProductListComponent } from './component/client/productList/productList.component';
export const routes: Routes = [
  { path: '', component: ProductUserComponent },
  { path: 'product/productList/:id', component: ProductListComponent },
  {
    path: 'admin',
    component: AdminComponent,
    children: [
      { path: '', component: DashboardComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'category', component: CategoryComponent },
      { path: 'product', component: ProductComponent },
      { path: 'product/productadd', component: ProductaddComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'quyen', component: RoleComponent },
    ],
  },
];
