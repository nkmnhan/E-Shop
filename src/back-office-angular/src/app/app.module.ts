import { HttpClientInterceptor } from './_services/interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './_components/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ComponentsModule } from './_modules/';
import { UploadComponent } from './_components/upload/upload.component';

@NgModule({
  declarations: [AppComponent, UploadComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
  ],
  providers: [HttpClientInterceptor],
  bootstrap: [AppComponent],
})
export class AppModule {}
