import { Component, EventEmitter, Input, input, Output, output } from '@angular/core';
import { TreeModule } from 'primeng/tree'
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SidebarModule as PrimeNgSidebarModule } from 'primeng/sidebar';
import { TreeNode } from 'primeng/api';
@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule, PrimeNgSidebarModule, TreeModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent{
  @Input() isSidebarCollapsed: boolean = false;
  @Output() changeIsSidebarCollapsed = new EventEmitter<boolean>();
  items = [
    {
      routerLink: 'dashboard',
      icon: 'pi pi-home',
      label: 'Dashboard',
    },
    {
      routerLink: 'category',
      icon: 'pi pi-book',
      label: 'Danh mục',
    },
    {
      routerLink: 'product',
      icon: 'pi pi-gift',
      label: 'Sản phẩm',

    },
    {
      routerLink: 'voucher',
      icon: 'pi pi-gift',
      label: 'Voucher',

    },
    {
      routerLink: 'chat',
      icon: 'pi pi-comments',
      label: 'Chat',
    },
    {
      routerLink: 'role',
      icon: 'pi pi-key',
      label: 'Quyền',
    },
    {
      routerLink: 'courter', // Đúng: 'routerLink'
      icon: 'pi pi-shopping-cart',
      label: 'Bán hàng tại quầy'
    },
    
    {
      routerLink: 'order',
      icon:'pi pi-check-square',
      label: 'Quản lý đơn hàng'
    }
  ];


  toggleCollapse(): void {
    this.changeIsSidebarCollapsed.emit(!this.isSidebarCollapsed);
  }

  closeSidenav(): void {
    this.changeIsSidebarCollapsed.emit(true);
  }
}
