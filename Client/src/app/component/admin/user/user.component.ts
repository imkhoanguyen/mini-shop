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
import { AuthService } from '../../../_services/auth.service';
import { TagModule } from 'primeng/tag';
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
    TooltipModule,
    TagModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  selectedUsers!: User[];
  roles: Role[] = [];
  selectedRole!: Role;
  visible: boolean = false;
  btnText: string = 'Cập nhật';
  headerText: string = 'Cập nhật Người dùng';
  userForm!: FormGroup;
  displayDialog: boolean = false;
  selectedUser!: User;
  lockUserForm!: FormGroup;
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
    private authService: AuthService
  ) {
    this.userForm = this.initializeForm();
    this.lockUserForm = this.initializeLockForm();
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
      password: [''],
      avatar: [''],
      role: ['', Validators.required],
    });
  }
  initializeLockForm(): FormGroup {
    return this.builder.group({
      id: [null],
      lockStatus: [false, Validators.required],
      minutes: [null],
      hours: [null],
      days: [null],
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
      this.selectedUsers.forEach((user) => {
        user.role = this.authService.getRoleFromToken(user.token);
      })
      console.log("selectedUsers", this.selectedUsers)
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
    console.log('User:', user);
    if (user) {
      this.userForm.reset();
      this.userForm.patchValue(user);
      this.btnText = 'Cập nhật';
      this.headerText = 'Cập nhật Người dùng';
    } else {
      this.userForm.reset();
      this.btnText = 'Thêm';
      this.headerText = 'Thêm Người dùng';
    }
  }
  showDialog(user: User): void {
    this.displayDialog = true;
    this.lockUserForm.patchValue({
      id: user.id,
      lockStatus: user.isLocked,
      minutes: null,
      hours: null,
      days: null,
    })
  }

  closeDialog(): void {
    this.displayDialog = false;
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

  onSubmit(): void {
    if (this.userForm.valid) {
    const formData = new FormData();
    formData.append('id', this.userForm.get('id')?.value);
    formData.append('fullName', this.userForm.get('fullName')?.value);
    formData.append('userName', this.userForm.get('userName')?.value);
    formData.append('email', this.userForm.get('email')?.value);
    formData.append('password', this.userForm.get('password')?.value);
    formData.append('role', this.userForm.get('role')?.value);
    if (this.selectedFile?.file) {
      formData.append('avatar', this.selectedFile.file);
    }
    console.log("idddd", this.userForm.get('id')?.value); // Kiểm tra giá trị ID

    const subscription = this.userForm.get('id')?.value === null
      ? this.accountService.addUser(formData).pipe(
          switchMap(() => this.accountService.getUsersPagedList({ pageNumber: this.pageNumber, pageSize: this.pageSize }))
        ).subscribe({
          next: (result) => {
            this.selectedUsers = result.items || [];
            this.toastService.success('Người dùng đã được thêm thành công.');
            this.visible = false;
          },
          error: (err) => {
            if (err.status === 400 && err.error) {
              const errorMessage = typeof err.error === 'string' ? err.error : 'Có lỗi xảy ra khi thêm.';
              this.toastService.error(errorMessage);
            } else {
              this.toastService.error('Có lỗi xảy ra khi thêm người dùng.');
            }
            console.error(err);
          }
        })
      : this.accountService.updateUser(formData).pipe(
          switchMap(() => this.accountService.getUsersPagedList({ pageNumber: this.pageNumber, pageSize: this.pageSize }))
        ).subscribe({
          next: (result) => {
            this.selectedUsers = Array.isArray(result.items) ? result.items : [];
            this.toastService.success('Người dùng đã được cập nhật thành công.');
            this.visible = false;
          },
          error: (err) => {
            if (err.status === 400 && err.error) {
              const errorMessage = typeof err.error === 'string' ? err.error : 'Có lỗi xảy ra khi cập nhật.';
              this.toastService.error(errorMessage);
            } else {
              this.toastService.error('Có lỗi xảy ra khi cập nhật người dùng.');
            }
            console.error(err);
          }
        });
      this.subscriptions.add(subscription);
    }
    if(this.lockUserForm.valid){
      const formValue = this.lockUserForm.value;
      const lockStatus = Boolean(formValue.lockStatus);

      console.log('Form Value:', formValue);
      console.log('Lock Status:', lockStatus);

      if (lockStatus) {
        const lockParams = {
          minutes: formValue.minutes,
          hours: formValue.hours,
          days: formValue.days,
        };
        this.accountService.lockUser(formValue.id, lockParams).subscribe({
          next: () => {
            this.toastService.success('Khóa người dùng thành công');
            this.displayDialog = false;
            this.loadUsers();
          },
          error: (err) => {
            this.toastService.error('Khóa người dùng thất bại');
            console.error(err);
          },
        });
      } else if (!lockStatus) {
        this.accountService.unlockUser(formValue.id).subscribe({
          next: () => {
            this.toastService.success('Mở khóa người dùng thành công');
            this.displayDialog = false;
            this.loadUsers();
          },
          error: (err) => {
            this.toastService.error('Mở khóa người dùng thất bại');
            console.error(err);
          },
        });
      } else {
        console.error('Giá trị lockStatus không hợp lệ:', lockStatus);
      }
    }else{
      this.toastService.error('Vui lòng kiểm tra lại thông tin');
    }
  }

  // removeImage(index: number) {
  //   this.selectedFile.splice(index, 1);
  // }



}


