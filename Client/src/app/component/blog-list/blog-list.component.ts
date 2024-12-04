import { Component } from '@angular/core';
import { TruncatePipe } from '../../layout/footerClient/truncate.pipe';
import { SafeHtmlPipe } from '../blog-user/safe-html.pipe';
import { BlogService } from '../../_services/blog.service';
import { Router } from '@angular/router';
import { Blog } from '../../_models/types';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blog-list',
  standalone: true,
  imports: [TruncatePipe, SafeHtmlPipe, CommonModule],
  templateUrl: './blog-list.component.html',
  styleUrl: './blog-list.component.css',
})
export class BlogListComponent {
  constructor(private blogService: BlogService, private router: Router) {}

  blogs: Blog[] = [];

  ngOnInit() {
    this.blogService.getAllBlogs().subscribe((res: Blog[]) => {
      this.blogs = res;
    });
  }

  navigateBlog(id: Number | undefined) {
    this.router.navigate(['blog', id]);
  }
}
