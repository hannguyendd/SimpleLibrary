import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';
import { BorrowedBook } from '../models/book.model';

@Injectable({
  providedIn: 'root',
})
export class UserService extends DataService {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'api/users');
  }

  getBooks() {
    return this.get<BorrowedBook[]>('me/borrowed-books');
  }
}
