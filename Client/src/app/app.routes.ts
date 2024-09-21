import { Routes } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { CategoryComponent } from './component/admin/category/category.component';
import { ProductComponent } from './component/admin/product/product.component';
import { ChatComponent } from './component/admin/chat/chat.component';
import { RoleComponent } from './component/admin/role/role.component';

export const routes: Routes = [
  {
    path: '', component: HeaderComponent,
  },
  {
    path: 'category', component: CategoryComponent,
  },
  {
    path: 'product', component: ProductComponent,
  },
  {
    path: 'chat', component: ChatComponent,
  },
  { path: 'admin/quyen', component: RoleComponent },
];
