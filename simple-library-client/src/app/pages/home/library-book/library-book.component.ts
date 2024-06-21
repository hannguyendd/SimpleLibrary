import { Component, OnInit, signal } from '@angular/core';
import { Book } from '../../../core/models/book.model';
import { BookService } from '../../../core/services/book.service';

@Component({
  selector: 'app-library-book',
  templateUrl: './library-book.component.html',
  styleUrl: './library-book.component.scss',
})
export class LibraryBookComponent implements OnInit {
  books = signal<Book[]>([]);

  constructor(private _bookService: BookService) {}

  ngOnInit(): void {
    this._bookService.getAll().subscribe((response) => {
      this.books.set(response);
    });
  }

  lendBook(book: Book) {
    this._bookService.lend(book.id).subscribe();
  }
}
