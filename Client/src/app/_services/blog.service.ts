import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Blog } from '../_models/types';
import { Observable } from 'rxjs';
import { Pagination } from '../_models/pagination.module';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  constructor(private http: HttpClient) {}

  apiUrl = environment.apiUrl;

  BlogList = signal<Blog[]>([]);
  BlogItems = signal<Blog>({
    title: '',
    content: '',
    category: '',
    userId: '',
    create: '',
    update: '',
  });

  addBlog(data: Blog): Observable<Blog> {
    return this.http.post<Blog>(this.apiUrl + '/Blog/Add', data, {
      headers: { 'Content-Type': 'application/json' },
    });
  }

  updateBlog(id: Number, data: Blog): Observable<any> {
    return this.http.put(this.apiUrl + '/Blog/Update/' + id, data, {
      headers: { 'Content-Type': 'application/json' },
    });
  }

  deleteBlog(data: Blog): Observable<any> {
    return this.http.delete(this.apiUrl + '/Blog/Delete/', { body: data });
  }
  getBlogById(id: Number) {
    return this.http.get<Blog>(this.apiUrl + '/Blog/GetById/' + id);
  }

  getAllBlogs() {
    return this.http.get<Blog[]>(this.apiUrl + '/Blog/GetAll');
  }
  getBlogNameById(BlogId: number) {
    return this.http.get(this.apiUrl + '/Blog/GetBlogNameById/' + BlogId);
  }

  getBlogsAllPaging(
    pageNumber: number,
    pageSize: number,
    searchString?: string
  ): Observable<Pagination<Blog>> {
    let param = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchString) {
      param = param.set('searchString', searchString);
    }
    return this.http.get<Pagination<Blog>>(this.apiUrl + '/Blog/GetAllPaging', {
      params: param,
    });
  }
}
