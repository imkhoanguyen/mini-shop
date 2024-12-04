import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { jwtInterceptor } from './_interceptors/jwt-interceptor.interceptor';
import { OAuthModule, OAuthService } from 'angular-oauth2-oidc';
import { importProvidersFrom } from '@angular/core';
import { MessageService } from 'primeng/api';
import { errorInterceptor } from './_interceptors/error.interceptor';
import { loadingInterceptor } from './_interceptors/loading.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([errorInterceptor, loadingInterceptor, jwtInterceptor])
    ),
    provideAnimationsAsync(),
    importProvidersFrom(OAuthModule.forRoot()),
    MessageService,
  ],
};
