import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TerminalModule } from 'primeng/terminal';
import {
  TableModule,
  TableRowCollapseEvent,
  TableRowExpandEvent,
} from 'primeng/table';
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
  ],
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css'],
  providers: [MessageService],
})
export class BlogComponent implements OnInit {
  Blogs: Blog[] = []; // Mảng chứa danh sách Blog
  expandedRows: { [key: number]: boolean } = {};
  searchQuery: string = '';
  constructor(private BlogService: BlogService) {}

  ngOnInit() {
    this.loadBlogs();
  }

  // Tải danh sách Blog từ service
  loadBlogs() {
    this.BlogService.getAllBlogs().subscribe(
      (data: Blog[]) => {
        this.Blogs = data;
        // this.checkBlogValidity();
      },
      (error) => {
        console.error('Error fetching ', error);
      }
    );
  }

  // checkBlogValidity() {
  //   const currentDate = new Date();
  //   this.Blogs.forEach((Blog) => {
  //     const BlogEndDate = new Date(Blog.end_date);
  //     if (BlogEndDate < currentDate && Blog.is_active) {
  //       Blog.is_active = false;
  //       this.onActiveChange(Blog);
  //       // console.log("hết hạn"+Blog.id)
  //     }
  //   });
  // }

  // onActiveChange(Blog: any) {
  //   // console.log(`Blog ID: ${Blog.id} is now ${Blog.is_active ? 'active' : 'inactive'}`);
  //   if (!Blog.is_active) {
  //     this.BlogService.deleteBlog(Blog.id).subscribe(
  //       (response) => {
  //         console.log('Blog deleted successfully:', response);
  //       },
  //       (error) => {
  //         console.error('Delete failed ', error);
  //       }
  //     );
  //   } else {
  //     // console.log(Blog.id)
  //     this.BlogService.restoreBlog(Blog.id).subscribe(
  //       (response) => {
  //         console.log('Blog restored successfully:', response);
  //       },
  //       (error) => {
  //         console.error('Restore failed ', error);
  //       }
  //     );
  //   }
  // }

  get filteredBlogs() {
    if (!this.searchQuery) {
      return this.Blogs; // If no search query, return all Blogs
    }
    return this.Blogs.filter(
      (Blog) =>
        Blog.title.toLowerCase().includes(this.searchQuery.toLowerCase())
      // || Blog.description.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }
}
