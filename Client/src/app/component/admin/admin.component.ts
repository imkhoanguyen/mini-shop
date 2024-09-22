import { Component, HostListener, signal } from '@angular/core';
import { SidebarComponent } from "./sidebar/sidebar.component";
import { MainComponent } from "./main/main.component";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [SidebarComponent, MainComponent, RouterOutlet],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {
  isSidebarCollapsed = signal<boolean>(false);
  screenWidth = signal<number>(window.innerWidth);
  @HostListener('window:resize')
  onResize() {
    this.screenWidth.set(window.innerWidth);
    if (this.screenWidth() < 768) {
      this.isSidebarCollapsed.set(true);
    }
  }
  ngOnInit(): void {
    this.isSidebarCollapsed.set(this.screenWidth() < 768);
  }

  changeIsSidebarCollapsed(isSidebarCollapsed: boolean): void {
    this.isSidebarCollapsed.set(isSidebarCollapsed);
  }
}
