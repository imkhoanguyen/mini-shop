import { Component, inject, OnInit } from '@angular/core';
import { RoleService } from '../../../_services/role.service';
import { Role } from '../../../_models/role.model';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [],
  templateUrl: './role.component.html',
  styleUrl: './role.component.css',
})
export class RoleComponent implements OnInit {
  private roleServices = inject(RoleService);
  roles: Role[] = [];
  ngOnInit(): void {
    this.loadRoles();
  }

  loadRoles() {
    this.roleServices.getRoles().subscribe({
      next: (response) => {
        this.roles = response;
      },
    });
  }
}
