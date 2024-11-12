import { Component } from '@angular/core';
import { Blog } from '../../_models/types';
import { ActivatedRoute } from '@angular/router';
import { BlogService } from '../../_services/blog.service';
import { DatePipe } from '@angular/common';
import { SafeHtmlPipe } from './safe-html.pipe';

@Component({
  selector: 'app-blog-user',
  standalone: true,
  imports: [SafeHtmlPipe],
  templateUrl: './blog-user.component.html',
  styleUrl: './blog-user.component.css',
  providers: [DatePipe],
})
export class BlogUserComponent {
  [x: string]: any;
  constructor(
    private route: ActivatedRoute,
    private blogService: BlogService,
    private datePipe: DatePipe
  ) {}
  ngOnInit() {
    this.blog.id = Number(this.route.snapshot.paramMap.get('id'));
    this.blogService.getBlogById(this.blog.id).subscribe({
      next: (res: Blog) => {
        this.blog = res;
        console.log(this.blog.title);
      },
    });
  }
  blog: Blog = {
    title: '',
    content: '',
    category: '',
    userId: '',
    create: '',
    update: '',
  };
  formatDate(
    date: Date | string,
    format: string = 'dd-MM-yyyy HH:mm:ss'
  ): string {
    return this.datePipe.transform(date, format) || '';
  }
}
