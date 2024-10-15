import { Component, ViewChild } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Sidebar, SidebarModule } from 'primeng/sidebar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuModule } from 'primeng/menu';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [SidebarModule, ButtonModule, PanelMenuModule, MenuModule, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  sidebarVisible: boolean = false;
  @ViewChild('sidebarRef') sidebarRef!: Sidebar;
  isAdmin = false;
  isLoggedIn = false;
  usernameOrEmail: string | null = null;
  closeCallback(e: Event): void {
      this.sidebarRef.close(e);
  }

  ngOnInit(): void {
    if (localStorage.getItem('role') === 'admin') {
      this.isAdmin = true;
    }
    if (localStorage.getItem('token') !== null) {
      this.isLoggedIn = true;
      console.log(this.isLoggedIn);
      this.usernameOrEmail = localStorage.getItem('userName');
      console.log(this.usernameOrEmail);
    }
  }
}
