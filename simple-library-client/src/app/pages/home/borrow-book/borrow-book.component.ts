import { Component, signal } from '@angular/core';
import { UserService } from '../../../core/services/user.service';
import { Book, BorrowedBook } from '../../../core/models/book.model';
import { BookService } from '../../../core/services/book.service';

@Component({
  selector: 'app-borrow-book',
  templateUrl: './borrow-book.component.html',
  styleUrl: './borrow-book.component.scss',
})
export class BorrowBookComponent {
  borrowedBooks = signal<BorrowedBook[]>([]);

  constructor(
    private _userService: UserService,
    private _bookService: BookService,
  ) {}

  ngOnInit(): void {
    this._userService.getBooks().subscribe((response) => {
      this.borrowedBooks.set(response);
    });
  }

  returnBook(borrowedBook: BorrowedBook) {
    this._bookService.return(borrowedBook.book.id).subscribe(response => {
      Object.assign(borrowedBook, response);
    })
  }
}
