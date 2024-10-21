import { Component, ViewChild } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Sidebar, SidebarModule } from 'primeng/sidebar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuModule } from 'primeng/menu';

@Component({
  selector: 'app-footer-client',
  standalone: true,
  imports: [SidebarModule, ButtonModule, PanelMenuModule, MenuModule],
  templateUrl: './footerClient.component.html',
})
export class FooterClientComponent {
  sidebarVisible: boolean = false;
  @ViewChild('sidebarRef') sidebarRef!: Sidebar;

  closeCallback(e: Event): void {
      this.sidebarRef.close(e);
  }
}
