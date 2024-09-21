import { Routes } from '@angular/router';
import { CategoryComponent } from './admin/category/category.component';
import { ProductComponent } from './admin/product/product.component';
import { ChatComponent } from './admin/chat/chat.component';
import { HeaderComponent } from './layout/header/header.component';

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
  }
];
