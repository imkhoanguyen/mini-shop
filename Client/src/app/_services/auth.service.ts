import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private decodeToken(token: string): any {
    const payload = token.split('.')[1];
    return JSON.parse(atob(payload));
  }

  hasClaim(claim: string): boolean {
    const token = localStorage.getItem('token');
    if (!token) return false;
    const decodedToken = this.decodeToken(token);
    console.log(decodedToken);
    console.log(decodedToken.Permission);
    return (
      decodedToken &&
      decodedToken.Permission &&
      decodedToken.Permission.includes(claim)
    );
  }

  hasRole(role: string): boolean {
    const token = localStorage.getItem('token');
    if (!token) return false;

    const decodedToken = this.decodeToken(token);

    return decodedToken && decodedToken.role === role;
  }
}
