import { Routes } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { CategoryComponent } from './component/admin/category/category.component';
import { ChatComponent } from './component/admin/chat/chat.component';
import { RoleComponent } from './component/admin/role/role.component';
import { AdminComponent } from './component/admin/admin.component';
import { DashboardComponent } from './component/admin/dashboard/dashboard.component';
import { ProductComponent } from './component/admin/product/product.component';
import { ProductUserComponent } from './component/client/product/product.component';
export const routes: Routes = [
  { path: '', component: ProductUserComponent },
  {
    path: 'admin',
    component: AdminComponent,
    children: [
      { path: '', component: DashboardComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'category', component: CategoryComponent },
      { path: 'product', component: ProductComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'quyen', component: RoleComponent },
    ],
  },
];
