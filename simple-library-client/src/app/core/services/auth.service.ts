import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest, TokenResponse } from '../models/auth.model';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends DataService {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'api/auth');
  }

  login(credentials: LoginRequest) {
    return this.post<TokenResponse>(credentials, 'login');
  }
}
