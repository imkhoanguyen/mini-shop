import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Sidebar, SidebarModule } from 'primeng/sidebar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuModule } from 'primeng/menu';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../_services/account.service';
import { Router } from '@angular/router';
import { User } from '../../_models/user.module';
import { ChatComponent } from "../../component/chat/chat.component";
import { ApiService } from '../../component/client/shared/api.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [SidebarModule, ButtonModule, PanelMenuModule, MenuModule, CommonModule, ChatComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  public cartItem:number = 0;
  sidebarVisible: boolean = false;
  @ViewChild('sidebarRef') sidebarRef!: Sidebar;
  private router = inject(Router);
  accountService = inject(AccountService);
  constructor(private api:ApiService) {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      const user = JSON.parse(userJson) as User;
      this.accountService.setCurrentUser(user);
    }
  }
  
  ngOnInit(): void {
this.api.products().subscribe(res =>
{
  this.cartItem=res.length
}
)

  }
  loginForm(){
    this.router.navigateByUrl('/login');
  }
  registerForm() {
    this.router.navigateByUrl('/register');
  }
  logout() {
    this.accountService.logout();
  }
}
