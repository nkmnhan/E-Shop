import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-auth-silent',
  templateUrl: './auth-silent.component.html',
  styleUrls: ['./auth-silent.component.scss'],
})
export class AuthSilentComponent implements OnInit {
  constructor(private authService: AuthService) {}

  async ngOnInit() {
    try {
      await this.authService.silientCallback();
    } catch (error) {
      console.log(error);
    }
  }
}
