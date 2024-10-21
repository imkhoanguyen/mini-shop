import { Component} from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { MainComponent } from './component/admin/main/main.component';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './component/admin/sidebar/sidebar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
    HeaderComponent,
    CommonModule,
    SidebarComponent,
    MainComponent],
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
