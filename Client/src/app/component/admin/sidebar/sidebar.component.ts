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
      routerLink: 'chat',
      icon: 'pi pi-comments',
      label: 'Chat',
    },
    {
      routerLink: 'role',
      icon: 'pi pi-key',
      label: 'Quyền',
    }
  ];


  toggleCollapse(): void {
    this.changeIsSidebarCollapsed.emit(!this.isSidebarCollapsed);
  }

  closeSidenav(): void {
    this.changeIsSidebarCollapsed.emit(true);
  }
}
