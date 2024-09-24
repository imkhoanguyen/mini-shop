import { Component, inject, OnInit } from '@angular/core';
import { RoleService } from '../../../_services/role.service';
import { Role } from '../../../_models/role.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { DialogModule } from 'primeng/dialog';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [
    ToastModule,
    DialogModule,
    ToastModule,
    FormsModule,
    ReactiveFormsModule,
    DatePipe,
    ConfirmPopupModule,
  ],
  templateUrl: './role.component.html',
  styleUrl: './role.component.css',
  providers: [MessageService, ConfirmationService],
})
export class RoleComponent implements OnInit {
  private roleServices = inject(RoleService);
  roles: Role[] = [];
  private fb = inject(FormBuilder);
  frm: FormGroup = new FormGroup({});
  visible: boolean = false;
  edit = false;
  private roleId?: string;

  constructor(
    private messageService: MessageService,
    private confirmationService: ConfirmationService
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

  loadRoles() {
    this.roleServices.getRoles().subscribe({
      next: (response) => (this.roles = response),
      error: (error) => this.showError(error.error),
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
        next: (_) => {
          const index: number = this.roles.findIndex((r) => r.id === role.id);
          this.roles[index].name = role.name;
          this.roles[index].description = role.description;
          this.showSuccess('Cập nhật quyền thành công');
          this.closeDialog();
        },
        error: (error) => this.showError(error.error),
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
          this.showSuccess('Thêm role thành công');
          this.closeDialog();
        },
        error: (error) => this.showError(error.error),
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
          this.showError('Không tìm thấy roleId');
          return;
        }

        this.roleServices.deleteRole(roleId).subscribe({
          next: (_) => {
            const index: number = this.roles.findIndex((r) => r.id === roleId);
            this.roles.splice(index, 1);
            this.showSuccess('Xóa thành công');
          },
          error: (error) => this.showError(error.error),
        });
      },
      reject: () => {
        this.showError('Bạn đã hủy xóa', 'Rejected');
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
  }

  showError(detail: string, summary?: string) {
    this.messageService.add({
      severity: 'error',
      summary: summary || 'Error',
      detail: detail,
      life: 3000,
    });
  }

  showSuccess(detail: string) {
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: detail,
      life: 3000,
    });
  }
}
