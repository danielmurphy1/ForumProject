import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from '../Board';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {
  //json-server server
  private apiUrl = 'http://localhost:5000';

  constructor(private http: HttpClient) { }

  getBoards(): Observable<Board[]> {
    return this.http.get<Board[]>(`${this.apiUrl}/boards`);
  }
}
