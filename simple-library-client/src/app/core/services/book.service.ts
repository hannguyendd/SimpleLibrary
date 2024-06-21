import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';
import { Book, BorrowedBook } from '../models/book.model';

@Injectable({
  providedIn: 'root',
})
export class BookService extends DataService {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'api/books');
  }

  getAll() {
    return this.get<Book[]>();
  }

  lend(bookId: string) {
    return this.post<BorrowedBook>({}, `${bookId}/lend`);
  }

  return(bookId: string) {
    return this.post<BorrowedBook>({}, `${bookId}/return`);
  }
}
