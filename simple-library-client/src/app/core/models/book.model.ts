export interface Book {
  id: string;
  name: string;
  price: number;
}

export interface BorrowedBook {
  book: Book;
  borrowedAt: Date;
  expiredAt: Date;
  returnedAt?: Date;
}
