import { Component, EventEmitter, Input, input, Output, output } from '@angular/core';

import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SidebarModule as PrimeNgSidebarModule } from 'primeng/sidebar';
@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule, PrimeNgSidebarModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  @Input() isSidebarCollapsed: boolean = false;
  @Output() changeIsSidebarCollapsed = new EventEmitter<boolean>();
  items = [
    {
      routeLink: 'dashboard',
      icon: 'pi pi-home',
      label: 'Dashboard',
    },
    {
      routeLink: 'category',
      icon: 'pi pi-book',
      label: 'Danh mục',
    },
    {
      routeLink: 'product',
      icon: 'pi pi-gift',
      label: 'Sản phẩm',
    },
    {
      routeLink: 'chat',
      icon: 'pi pi-comments',
      label: 'Chat',
    }
  ];

  toggleCollapse(): void {
    this.changeIsSidebarCollapsed.emit(!this.isSidebarCollapsed);
  }

  closeSidenav(): void {
    this.changeIsSidebarCollapsed.emit(true);
  }
}
