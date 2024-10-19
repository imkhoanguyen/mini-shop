import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Sidebar, SidebarModule } from 'primeng/sidebar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuModule } from 'primeng/menu';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [SidebarModule, ButtonModule, PanelMenuModule, MenuModule, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {

  sidebarVisible: boolean = false;
  @ViewChild('sidebarRef') sidebarRef!: Sidebar;
  private router = inject(Router);
  accountService = inject(AccountService);
  userInfo: any;
  ngOnInit(): void {
    this.userInfo = JSON.parse(localStorage.getItem('userInfo') || '{}');

  }
  loginForm(){
    this.router.navigateByUrl('/login');
  }
  registerForm(){
    this.router.navigateByUrl('/register');
  }
  logout(){
    this.accountService.logout();
  }
}
