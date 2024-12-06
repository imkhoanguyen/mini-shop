import { Component, ViewChild } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Sidebar, SidebarModule } from 'primeng/sidebar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuModule } from 'primeng/menu';
import { BlogService } from '../../_services/blog.service';
import { Blog } from '../../_models/types';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-footer-client',
  standalone: true,
  imports: [
    SidebarModule,
    ButtonModule,
    PanelMenuModule,
    MenuModule,
    CommonModule,
  ],
  templateUrl: './footerClient.component.html',
  styleUrl: './footerClient.component.css',
})
export class FooterClientComponent {
  constructor(private blogService: BlogService, private router: Router) {}
  sidebarVisible: boolean = false;
  @ViewChild('sidebarRef') sidebarRef!: Sidebar;

  closeCallback(e: Event): void {
    this.sidebarRef.close(e);
  }

  blogs: Blog[] = [];

  ngOnInit() {
    this.blogService.get5Blog().subscribe((res: Blog[]) => {
      this.blogs = res;
    });
  }

  navigate() {
    this.router.navigate(['blog-list']);
  }

  navigateBlog(id: Number | undefined) {
    this.router.navigate(['blog', id]);
  }
}
