import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private decodeToken(token: string): any {
    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));
  }

  hasClaim(claim: string): boolean {
    const userString = localStorage.getItem('user');
    let token = null;

    if (userString) {
      const user = JSON.parse(userString);
      token = user.token;
    }
    if (!token) return false;
    const decodedToken = this.decodeToken(token);
    // console.log(decodedToken);
    // console.log(decodedToken.Permission);
    return (
      decodedToken &&
      decodedToken.Permission &&
      decodedToken.Permission.includes(claim)
    );
  }

  hasRole(role: string): boolean {
    const userString = localStorage.getItem('user');
    let token = null;

    if (userString) {
      const user = JSON.parse(userString);
      token = user.token;
    }
    if (!token) return false;

    const decodedToken = this.decodeToken(token);

    return decodedToken && decodedToken.role === role;
  }
  getRoleFromToken(token: string): string {
    try {
      const decodedToken: any = jwtDecode(token);
      return decodedToken.role;
    } catch (error) {
      console.error('Token không hợp lệ', error);
      return '';
    }
  }
}
