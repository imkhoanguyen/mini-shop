import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from '../_services/toastr.service';
import { map, catchError, of, from } from 'rxjs';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  const toastrService = inject(ToastrService);

  return from(accountService.isCustomerRole()).pipe(
    map((isCustomer) => {
      const currentUser = accountService.getCurrentUser();
      if (currentUser && !isCustomer) {
        return true;
      }
      toastrService.error('Quyền truy cập bị từ chối');
      router.navigate(['/']);
      return false;
    }),
    catchError(() => {
      toastrService.error('Có lỗi xảy ra, vui lòng thử lại');
      router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return of(false);
    })
  );
};
