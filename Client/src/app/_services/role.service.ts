import { HttpClient } from '@angular/common/http';
import { inject, Injectable, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { Role } from '../_models/role.model';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getRoles() {
    return this.http.get<Role[]>(this.baseUrl + '/role');
  }

  addRole(role: Role) {
    return this.http.post<Role>(this.baseUrl + '/role', role);
  }

  updateRole(roleId: string, role: Role) {
    return this.http.put(`${this.baseUrl}/role/${roleId}`, role);
  }

  deleteRole(roleId: string) {
    return this.http.delete(`${this.baseUrl}/role/${roleId}`);
  }
}
