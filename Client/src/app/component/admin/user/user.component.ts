import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { User } from '../../../_models/user.module';
import { Pagination } from '../../../_models/pagination';
import { Subscription, switchMap } from 'rxjs';
import { AccountService } from '../../../_services/account.service';
import { ToastrService } from '../../../_services/toastr.service';
import { RoleService } from '../../../_services/role.service';
import { TooltipModule } from 'primeng/tooltip';
import { Role } from '../../../_models/role';
@Component({
  selector: 'app-user',
  standalone: true,
  imports: [ButtonModule,
    CheckboxModule,
    CommonModule,
    TableModule,
    DropdownModule,
    InputTextModule,
    DialogModule,
    ToastModule,
    ReactiveFormsModule,
    PaginatorModule,
    ConfirmDialogModule,
    TooltipModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  selectedUsers!: User[];
  roles: Role[] = [];
  selectedRole!: Role;
  visible: boolean = false;
  btnText: string = 'Thêm';
  headerText: string = 'Thêm Người dùng';
  userForm!: FormGroup;

  pageSizeOptions = [
    { label: '5', value: 5 },
    { label: '10', value: 10 },
    { label: '20', value: 20 },
    { label: '50', value: 50 },
  ];

  first: number = 0;
  pagination: Pagination = { currentPage: 1, itemPerPage: 10, totalItems: 0, totalPages: 1 };
  totalRecords: number = 0;
  pageSize: number = 5;
  pageNumber: number = 1;
  searchString: string = "";
  selectedFile: { src: string; file: File; } | undefined;

  private subscriptions: Subscription = new Subscription();

  constructor(
    private builder: FormBuilder,
    private accountService: AccountService,
    private roleService: RoleService,
    private toastService: ToastrService,
  ) {
    this.userForm = this.initializeForm();
  }

  ngOnInit(): void {
    this.loadUsers();
    this.loadRoles();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  initializeForm(): FormGroup {
    return this.builder.group({
      id: [0],
      fullName: ['', Validators.required],
      userName: ['', Validators.required],
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required],
      avatar: [''],
      role: ['', Validators.required],
    });
  }

  loadUsers(): void {
    const params = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      search: this.searchString
    };
    this.accountService.getUsersPagedList(params).subscribe((result) => {
      this.selectedUsers = result.items || [];
      this.pagination = result.pagination ?? { currentPage: 1, itemPerPage: 10, totalItems: 0, totalPages: 1 };

      this.totalRecords = this.pagination.totalItems;
      this.first = (this.pageNumber - 1) * this.pageSize;
    });
  }
  loadRoles(): void {
    this.roleService.getAllRoles().subscribe((roles) => {
      this.roles = roles;
    });
  }

  onPageChange(event: any): void {
    this.first = event.first;
    this.pageNumber = event.page + 1;
    this.pageSize = event.rows;


    this.loadUsers();
  }

  onPageSizeChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    if (target) {
      this.pageSize = +target.value;
      this.pageNumber = 1;
      this.loadUsers();
    }
  }

  onSearch(): void {
    this.pageNumber = 1;
    console.log("search", this.searchString)
    this.loadUsers();
  }

  openDialog(user?: User): void {
    this.visible = true;
    if (user) {
      this.userForm.patchValue(user);
      this.btnText = 'Cập nhật';
      this.headerText = 'Cập nhật Người dùng';
    } else {
      this.userForm.reset();
      this.btnText = 'Thêm';
      this.headerText = 'Thêm Người dùng';
    }
  }

  onSubmit(): void {
    if (this.userForm.invalid) {
      this.toastService.error('Vui lòng điền đầy đủ thông tin.');
      return;
    }

    const userData = { ...this.userForm.value, id: this.userForm.value.id || 0 };

    const subscription = userData.id === 0
      ? this.accountService.register(userData).pipe(
          switchMap(() => this.accountService.getUsersPagedList({ pageNumber: this.pageNumber, pageSize: this.pageSize }))
        ).subscribe({
          next: (result) => {
            this.selectedUsers = result.items || [];
            this.toastService.success('Danh mục đã được thêm thành công.');
            this.visible = false;
          },
          error: (err) => {
            this.toastService.error('Có lỗi xảy ra khi thêm người dùng.');
            console.error(err);
          }
        })
      : this.accountService.updateUser(userData).pipe(
          switchMap(() => this.accountService.getUsersPagedList({ pageNumber: this.pageNumber, pageSize: this.pageSize }))
        ).subscribe({
          next: (result) => {
            this.selectedUsers = Array.isArray(result.items) ? result.items : [];
            this.toastService.success('Người dùng đã được cập nhật thành công.');
            this.visible = false;
          },
          error: (err) => {
            this.toastService.error('Có lỗi xảy ra khi cập nhật người dùng.');
            console.error(err);
          }
        });

    this.subscriptions.add(subscription);
  }
  onFileSelected(event: any) {
    const file: FileList = event.target.files;
    if (file && file[0]) {
      const reader = new FileReader();
  
      reader.onload = (e: any) => {
        this.selectedFile = {
          src: e.target.result,
          file: file[0],
        };
      };
  
      reader.readAsDataURL(file[0]);
    }
  }
  // removeImage(index: number) {
  //   this.selectedFile.splice(index, 1);
  // }


  
}
