import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, retry, catchError, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';

export class DataService {
  protected _baseUrl: string;

  constructor(protected httpClient: HttpClient, baseEndpoint: string) {
    const url = environment.url;
    this._baseUrl = `${url}/${baseEndpoint}`;
  }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  get<T>(endpoint = ''): Observable<T> {
    return this.httpClient
      .get<T>(`${this._baseUrl}/${endpoint}`)
      .pipe(retry(2), catchError(this.handleError));
  }

  // getById(id: number): Observable<T> {
  //   return this.httpClient
  //     .get<T>(`${this.url}/${this.endpoint}/${id}`)
  //     .pipe(retry(2), catchError(this.handleError));
  // }

  post<T>(body: any | null = null, endpoint: string = ''): Observable<T> {
    return this.httpClient
      .post<T>(`${this._baseUrl}/${endpoint}`, body, this.httpOptions)
      .pipe(retry(2), catchError(this.handleError));
  }

  update<TId, T>(id: TId, item: T): Observable<T> {
    return this.httpClient
      .put<T>(`${this._baseUrl}}/${id}`, JSON.stringify(item), this.httpOptions)
      .pipe(retry(2), catchError(this.handleError));
  }

  delete<IId>(id: IId) {
    return this.httpClient
      .delete(`${this._baseUrl}/${id}`, this.httpOptions)
      .pipe(retry(2), catchError(this.handleError));
  }
  //#endregion

  //#region [ Private ]
  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      //error client
      errorMessage = error.error.message;
    } else {
      //error server
      errorMessage = `${error.status}: ` + `${error.message}`;
    }

    return throwError(() => new Error(errorMessage));
  }
}
