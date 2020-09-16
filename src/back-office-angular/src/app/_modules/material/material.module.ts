import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';

const MaterialComponents = [MatToolbarModule,
    MatIconModule];

@NgModule({
  declarations: [],
  imports: [
    MaterialComponents
  ],
  exports:[MaterialComponents]
})
export class MaterialModule { }
