import { Component, inject, OnInit } from '@angular/core';
import { RoleService } from '../../../_services/role.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { DialogModule } from 'primeng/dialog';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { Table, TableModule } from 'primeng/table';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { Role } from '../../../_models/role';
import { Pagination } from '../../../_models/pagination';
import { PaginatorModule } from 'primeng/paginator';
import { Router } from '@angular/router';
import { AuthService } from '../../../_services/auth.service';
import { ToastrService } from '../../../_services/toastr.service';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [
    DialogModule,
    ToastModule,
    FormsModule,
    ReactiveFormsModule,
    DatePipe,
    ConfirmPopupModule,
    PaginatorModule,
    TableModule,
    CommonModule,
  ],
  templateUrl: './role.component.html',
  styleUrl: './role.component.css',
  providers: [ConfirmationService],
})
export class RoleComponent implements OnInit {
  private roleServices = inject(RoleService);
  roles: Role[] = [];
  private fb = inject(FormBuilder);
  private router = inject(Router);
  frm: FormGroup = new FormGroup({});
  visible: boolean = false;
  edit = false;
  pagination: Pagination | undefined;
  pageNumber = 1;
  pageSize = 5;
  search: string = '';
  private roleId?: string;
  private toasrt = inject(ToastrService);
  validationErrors?: string[];

  constructor(
    private confirmationService: ConfirmationService,
    public authServices: AuthService
  ) {}

  ngOnInit(): void {
    this.loadRoles();
    this.initForm();
  }

  initForm() {
    this.frm = this.fb.group({
      name: new FormControl<string>('', [Validators.required]),
      description: new FormControl<string>('', [Validators.required]),
    });
  }

  onPageChange(event: any) {
    this.pageNumber = event.page + 1;
    this.pageSize = event.rows;
    this.loadRoles();
  }

  onSearch() {
    // reset pageNumber
    this.pageNumber = 1;
    this.loadRoles();
  }

  loadRoles() {
    this.roleServices
      .getRoles(this.pageNumber, this.pageSize, this.search)
      .subscribe({
        next: (response) => {
          if (response.items && response.pagination) {
            this.roles = response.items;
            this.pagination = response.pagination;
          }
        },
      });
  }

  onSubmit() {
    if (this.edit === true && this.roleId) {
      // edit
      const role: Role = {
        id: this.roleId,
        name: this.frm.value.name,
        description: this.frm.value.description,
      };
      this.roleServices.updateRole(role.id || '', role).subscribe({
        next: (response) => {
          const index: number = this.roles.findIndex((r) => r.id === role.id);
          this.roles[index] = response;
          this.toasrt.success('Cập nhật quyền thành công');
          this.closeDialog();
        },
        error: (er) => {
          this.validationErrors = er;
        },
      });
    } else {
      // add
      const role: Role = {
        name: this.frm.value.name,
        description: this.frm.value.description,
      };
      this.roleServices.addRole(role).subscribe({
        next: (response) => {
          this.roles.unshift(response);
          this.toasrt.success('Thêm quyền thành công');
          this.closeDialog();
        },
        error: (er) => {
          this.validationErrors = er;
        },
      });
    }
  }

  deleteConfirmPopup(event: Event, roleId?: string) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Bạn muốn xóa dòng này?',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-sm',
      accept: () => {
        if (!roleId) {
          this.toasrt.error('Không tìm thấy roleId');
          return;
        }

        this.roleServices.deleteRole(roleId).subscribe({
          next: (_) => {
            const index: number = this.roles.findIndex((r) => r.id === roleId);
            this.roles.splice(index, 1);
            this.toasrt.success('Xóa thành công');
          },
          error: (error) => console.log(error),
        });
      },
      reject: () => {
        this.toasrt.info('Bạn đã hủy xóa');
      },
    });
  }

  showDialog(roleId?: string) {
    // edit
    if (roleId != null) {
      this.roleId = roleId;
      this.edit = true;
      const roleEdit = this.roles.find((r) => r.id === roleId);
      this.frm.patchValue({
        id: roleEdit?.id,
        name: roleEdit?.name,
        description: roleEdit?.description,
      });
      this.visible = true;
    } else {
      // add
      this.visible = true;
    }
  }

  closeDialog() {
    this.frm.reset();
    this.visible = false;
    this.edit = false;
    this.validationErrors = [];
  }

  onGoRolePermission(roleId: string) {
    this.router.navigate(['/admin/role/permission', roleId]);
  }
}
