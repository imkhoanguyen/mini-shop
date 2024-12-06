import {
  ChangeDetectorRef,
  Component,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Sidebar, SidebarModule } from 'primeng/sidebar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuModule } from 'primeng/menu';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../_services/account.service';
import { Router } from '@angular/router';
import { User } from '../../_models/user.module';
import { ChatComponent } from '../../component/client/chat/chat.component';
import { CartService } from '../../_services/cart.service';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    SidebarModule,
    ButtonModule,
    PanelMenuModule,
    MenuModule,
    CommonModule,
    ChatComponent,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  public cartItem: number = 0;
  sidebarVisible: boolean = false;
  isCustomer: boolean = false;
  isLoggedIn: boolean = false;
  @ViewChild('sidebarRef') sidebarRef!: Sidebar;
  private router = inject(Router);
  accountService = inject(AccountService);
  private authService = inject(AuthService);
  constructor() {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      const user = JSON.parse(userJson) as User;
      this.accountService.setCurrentUser(user);
    }
  }

  cartService = inject(CartService);

  ngOnInit(): void {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      const user = JSON.parse(userJson) as User;
      this.accountService.setCurrentUser(user);
      this.isLoggedIn = true;

      if (this.authService.hasClaim('Access.Admin')) {
        this.isCustomer = true;
      }
    } else {
      this.isLoggedIn = false;
      this.isCustomer = false;
    }
  }

  logout(): void {
    this.accountService.logout();
    this.isLoggedIn = false;
    this.isCustomer = false;
    this.router.navigate(['/login']);
  }
  loginForm() {
    this.router.navigateByUrl('/login');
  }
  registerForm() {
    this.router.navigateByUrl('/register');
  }
}
