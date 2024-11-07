import { HttpInterceptorFn } from '@angular/common/http';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const userString = localStorage.getItem('user');
  let token = null;

  if (userString) {
    try {
      const user = JSON.parse(userString);
      token = user.token;
    } catch (error) {
      console.error('Error parsing user data:', error);
    }
  }

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  return next(req);
};
