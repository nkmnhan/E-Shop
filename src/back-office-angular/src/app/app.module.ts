
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './_components/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeroesComponent } from './_components/heroes/heroes.component';
import { FormsModule } from '@angular/forms';
import { HeroDetailComponent } from './_components/hero-detail/hero-detail.component';
import { MessagesComponent } from './_components/messages/messages.component';
import { DashboardComponent } from './_components/dashboard/dashboard.component';

import { HttpClientModule } from '@angular/common/http';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { InMemoryDataService } from './_services/in-memory-data.service';
import { HeroSearchComponent } from './_components/hero-search/hero-search.component';
import { NavBarComponent } from './_components/nav-bar/nav-bar.component';
import { MaterialModule } from './_modules';
import { AuthCallbackComponent } from './_components/auth-callback/auth-callback.component';
import { AuthSilentComponent } from './_components/auth-silent/auth-silent.component';

@NgModule({
  declarations: [
    AppComponent,
    HeroesComponent,
    HeroDetailComponent,
    MessagesComponent,
    DashboardComponent,
    HeroSearchComponent,
    NavBarComponent,
    AuthCallbackComponent,
    AuthSilentComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    HttpClientInMemoryWebApiModule.forRoot(InMemoryDataService, {
      dataEncapsulation: false,
    }),
    MaterialModule,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
