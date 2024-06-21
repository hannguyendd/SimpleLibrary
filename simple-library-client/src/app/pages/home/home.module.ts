import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { BorrowBookComponent } from './borrow-book/borrow-book.component';
import { LibraryBookComponent } from './library-book/library-book.component';

@NgModule({
  declarations: [HomeComponent, BorrowBookComponent, LibraryBookComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: 'your-books', component: BorrowBookComponent },
      { path: 'library-books', component: LibraryBookComponent },
      { path: '', redirectTo: 'your-books', pathMatch: 'full' },
    ]),
  ],
})
export class HomeModule {}
