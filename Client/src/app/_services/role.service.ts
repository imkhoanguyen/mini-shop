import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { PermissionGroup, Role } from '../_models/role';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<Role[]> = new PaginatedResult<Role[]>();

  getRoles(page?: number, itemPerPage?: number, search?: string) {
    let params = new HttpParams();
    if (page && itemPerPage) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemPerPage);
    }

    if (search) params = params.append('query', search);

    return this.http
      .get<Role[]>(this.baseUrl + '/role', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          if (response.body) {
            this.paginatedResult.items = response.body;
          }
          const pagination = response.headers.get('Pagination');
          if (pagination) {
            this.paginatedResult.pagination = JSON.parse(pagination);
          }
          return this.paginatedResult;
        })
      );
  }

  addRole(role: Role) {
    return this.http.post<Role>(this.baseUrl + '/role', role);
  }

  updateRole(roleId: string, role: Role) {
    return this.http.put<Role>(`${this.baseUrl}/role/${roleId}`, role);
  }

  deleteRole(roleId: string) {
    return this.http.delete(`${this.baseUrl}/role/${roleId}`);
  }

  getAllPermission() {
    return this.http.get<PermissionGroup[]>(this.baseUrl + '/role/permissions');
  }

  getRole(roleId: string) {
    return this.http.get<Role>(this.baseUrl + `/role/${roleId}`);
  }

  getRoleClaims(roleId: string) {
    return this.http.get<string[]>(this.baseUrl + `/role/claims/${roleId}`);
  }

  updateRoleClaim(roleId: string, listClaimValue: string[]) {
    return this.http.put(
      this.baseUrl + `/role/update-claims/${roleId}`,
      listClaimValue
    );
  }
}
