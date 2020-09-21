import { from } from 'rxjs';
import { NgModule } from '@angular/core';
import { HeroDetailComponent } from '../../_components/hero-detail/hero-detail.component';
import { MessagesComponent } from '../../_components/messages/messages.component';
import { DashboardComponent } from '../../_components/dashboard/dashboard.component';

import { HeroesComponent } from '../../_components/heroes/heroes.component';
import { HeroSearchComponent } from '../../_components/hero-search/hero-search.component';
import { NavBarComponent } from '../../_components/nav-bar/nav-bar.component';
import { AuthCallbackComponent } from '../../_components/auth-callback/auth-callback.component';
import { AuthSilentComponent } from '../../_components/auth-silent/auth-silent.component';
import { VideoComponent } from '../../_components/video/video.component';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    HeroesComponent,
    HeroDetailComponent,
    MessagesComponent,
    DashboardComponent,
    HeroSearchComponent,
    NavBarComponent,
    AuthCallbackComponent,
    AuthSilentComponent,
    VideoComponent,
  ],
  imports: [MatToolbarModule, MatIconModule],
  exports: [NavBarComponent, VideoComponent],
  entryComponents: [NavBarComponent, VideoComponent],
})
export class ComponentsModule {}
