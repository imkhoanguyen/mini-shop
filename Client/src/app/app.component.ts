import { Component, HostListener, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthComponent } from './component/auth/auth.component';
import { HeaderComponent } from "./layout/header/header.component";
import { SidebarComponent } from "./component/admin/sidebar/sidebar.component";
import { MainComponent } from './component/admin/main/main.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AuthComponent, HeaderComponent, CommonModule, SidebarComponent, MainComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Client';

  constructor(private router: Router) {}

  isAdminPage(): boolean {
    return this.router.url.startsWith('/admin');
  }

}
