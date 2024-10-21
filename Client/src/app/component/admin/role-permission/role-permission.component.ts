import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PermissionGroup, Role } from '../../../_models/role';
import { RoleService } from '../../../_services/role.service';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-role-permission',
  standalone: true,
  imports: [ToastModule],
  templateUrl: './role-permission.component.html',
  styleUrl: './role-permission.component.css',
  providers: [MessageService],
})
export class RolePermissionComponent implements OnInit {
  roleId: string = '';
  private route = inject(ActivatedRoute);
  private roleServices = inject(RoleService);
  permissionGroups: PermissionGroup[] = [];
  roleClaims: string[] = [];
  role: Role | undefined;

  constructor(private messageService: MessageService) {}

  ngOnInit(): void {
    this.roleId = this.route.snapshot.paramMap.get('id')!;
    this.loadRole();
    this.loadPermissions();
    this.loadRoleClaims();
  }

  loadPermissions() {
    this.roleServices.getAllPermission().subscribe({
      next: (data) => {
        this.permissionGroups = data;
      },
      error: (er) => console.log(er),
    });
  }

  loadRole() {
    this.roleServices.getRole(this.roleId).subscribe({
      next: (data) => {
        this.role = data;
      },
    });
  }

  loadRoleClaims() {
    this.roleServices.getRoleClaims(this.roleId).subscribe({
      next: (data) => {
        this.roleClaims = data;
      },
      error: (er) => console.log(er),
    });
  }

  onClaimToggle(claimValue: string, event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;

    if (isChecked) {
      if (!this.roleClaims.includes(claimValue)) {
        this.roleClaims.push(claimValue);
      }
    } else {
      this.roleClaims = this.roleClaims.filter((c) => c !== claimValue);
    }
  }

  saveRoleClaims() {
    this.roleServices.updateRoleClaim(this.roleId, this.roleClaims).subscribe({
      next: () => {
        this.showSuccess('Quyền đã được cập nhật thành công.');
      },
      error: (er) => {
        this.showError('Có lỗi xảy ra khi cập nhật quyền:', er);
      },
    });
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
