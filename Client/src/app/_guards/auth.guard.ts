import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from '../_services/toastr.service';
import { map, catchError, of, from } from 'rxjs';
import { AuthService } from '../_services/auth.service';

export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const accountService = inject(AccountService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const toastrService = inject(ToastrService);

  const currentUser = accountService.getCurrentUser();
  const isAccessAdminPage = authService.hasClaim('Access.Admin');

  if (currentUser && isAccessAdminPage) {
    return true;
  }

  if (!currentUser) {
    toastrService.error('Bạn cần đăng nhập');
    router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
  } else {
    toastrService.error('Quyền truy cập bị từ chối');
    router.navigate(['/']);
  }
  return false;
};
