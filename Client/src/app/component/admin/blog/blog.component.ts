import { Component, OnInit } from '@angular/core';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TerminalModule } from 'primeng/terminal';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';
import { ToastModule } from 'primeng/toast';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { RouterModule } from '@angular/router';
import { InputSwitchModule } from 'primeng/inputswitch';
import { Blog } from '../../../_models/types';
import { BlogService } from '../../../_services/blog.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
@Component({
  selector: 'app-Blog',
  standalone: true,
  imports: [
    TabViewModule,
    TagModule,
    TerminalModule,
    TableModule,
    ButtonModule,
    RatingModule,
    ToastModule,
    CommonModule,
    FormsModule,
    RouterModule,
    InputSwitchModule,
    ConfirmDialogModule,
  ],
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css'],
  providers: [MessageService, ConfirmationService],
})
export class BlogComponent implements OnInit {
  Blogs: Blog[] = []; // Mảng chứa danh sách Blog
  expandedRows: { [key: number]: boolean } = {};
  searchQuery: string = '';
  constructor(
    private BlogService: BlogService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.loadBlogs();
  }

  // Tải danh sách Blog từ service
  loadBlogs() {
    this.BlogService.getAllBlogs().subscribe(
      (data: Blog[]) => {
        this.Blogs = data;
      },
      (error) => {
        console.error('Error fetching ', error);
      }
    );
  }

  confirmDelete(blogId: Number) {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xóa mục này?',
      header: 'Xác nhận',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.BlogService.deleteBlog(blogId).subscribe((res) => {
          this.loadBlogs();
        });
      },
      reject: () => {},
    });
  }

  get filteredBlogs() {
    if (!this.searchQuery) {
      return this.Blogs; // If no search query, return all Blogs
    }
    return this.Blogs.filter((Blog) =>
      Blog.title.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }
}
