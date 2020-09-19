import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HeroesComponent } from './_components/heroes/heroes.component';
import { DashboardComponent } from './_components/dashboard/dashboard.component';
import { HeroDetailComponent } from './_components/hero-detail/hero-detail.component';
import { AuthGuardService } from './_guards/auth-guard.service';
import { AuthService } from './_services/auth.service';
import { AuthCallbackComponent } from './_components/auth-callback/auth-callback.component';
import { AuthSilentComponent } from './_components/auth-silent/auth-silent.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuardService],
  },
  { path: 'detail/:id', component: HeroDetailComponent },
  { path: 'heroes', component: HeroesComponent },
  { path: 'callback', component: AuthCallbackComponent },
  { path: 'silent', component: AuthSilentComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuardService, AuthService],
})
export class AppRoutingModule {}
