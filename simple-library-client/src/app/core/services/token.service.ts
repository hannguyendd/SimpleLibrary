import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private readonly _tokenKey = 'token';

  private _token: string = '';

  public get token(): string {
    if (!this._token) {
      this._token = localStorage.getItem(this._tokenKey) ?? '';
    }

    return this._token;
  }

  public set token(value: string) {
    this._token = value ?? '';
    localStorage.setItem(this._tokenKey, this._token);
  }

  // public isAuthenticated(): boolean {
  //   // get the token
  //   const token = this.getToken();
  //   // return a boolean reflecting
  //   // whether or not the token is expired
  //   return tokenNotExpired(null, token);
  // }
}
