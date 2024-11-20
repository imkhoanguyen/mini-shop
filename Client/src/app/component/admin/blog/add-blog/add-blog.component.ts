// import { Component, OnInit } from '@angular/core';
// import {
//   FormBuilder,
//   FormGroup,
//   ReactiveFormsModule,
//   Validators,
// } from '@angular/forms';
// import { BlogService } from '../../../../_services/blog.service';

// import { ButtonModule } from 'primeng/button';
// import { InputTextModule } from 'primeng/inputtext';
// import { CalendarModule } from 'primeng/calendar';
// import { ActivatedRoute, Router, RouterModule } from '@angular/router';
// import { NgIf } from '@angular/common';
// import { Blog } from '../../../../_models/types';
// // import { EditorModule } from '@tinymce/tinymce-angular';

// @Component({
//   selector: 'app-add-blog',
//   standalone: true,
//   templateUrl: './add-blog.component.html',
//   styleUrls: ['./add-blog.component.css'],
//   // imports: [
//   //   ReactiveFormsModule,
//   //   ButtonModule,
//   //   InputTextModule,
//   //   CalendarModule,
//   //   RouterModule,
//   //   NgIf,
//   //   EditorModule,
//   // ],
// })
// export class AddBlogComponent implements OnInit {
//   blogForm!: FormGroup;
//   currentUrl: string = '';
//   id!: number;
//   blog!: Blog;

//   constructor(
//     private formBuilder: FormBuilder,
//     private blogService: BlogService,
//     private router: Router,
//     private route: ActivatedRoute
//   ) {
//     this.currentUrl = this.router.url;
//     this.blogForm = this.formBuilder.group({
//       title: ['', Validators.required],
//       content: ['', Validators.required],
//       category: ['', Validators.required],
//       userId: ['', Validators.required],
//       create: ['', Validators.required],
//       update: ['', Validators.required],
//     });
//   }

//   ngOnInit(): void {
//     this.route.params.subscribe((params) => {
//       this.id = +params['id'];
//       if (this.currentUrl !== '/admin/blog/new') {
//         this.getBlogById();
//       }
//     });
//   }

//   getBlogById() {
//     this.blogService.getBlogById(this.id).subscribe(
//       (data: Blog) => {
//         this.blog = data as Blog;
//         this.blogForm.patchValue({
//           title: this.blog.title,
//           content: this.blog.content,
//           category: this.blog.category,
//           userId: this.blog.userId,
//           create: this.blog.create,
//           update: this.blog.update,
//         });
//       },
//       (error: any) => {
//         console.error('Error fetching blog:', error);
//       }
//     );
//   }

//   onSubmit(): void {
//     this.blog = this.blogForm.value;
//     if (this.blogForm.valid) {
//       if (this.currentUrl === '/admin/blog/new') {
//         this.blogService.addBlog(this.blog).subscribe({
//           next: (response) => {
//             console.log('Blog added successfully:', response);
//             this.router.navigate(['/admin/blog']);
//           },
//           error: (err) => {
//             console.error('Failed to add blog:', err);
//           },
//         });
//       } else {
//         this.blogService.updateBlog(this.id, this.blog).subscribe({
//           next: (response) => {
//             console.log('Blog updated successfully:', response);
//             this.router.navigate(['/admin/blog']);
//           },
//           error: (err) => {
//             console.error('Failed to update blog:', err);
//           },
//         });
//       }
//     } else {
//       console.log('Form is invalid!');
//     }
//   }
// }
