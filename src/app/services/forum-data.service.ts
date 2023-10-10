import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from '../models/Board';
import { HttpClient } from '@angular/common/http';
import { Post } from '../models/Post';

@Injectable({
  providedIn: 'root'
})
export class ForumDataService {
  //json-server server
  private apiUrl = 'http://localhost:5000';

  constructor(private http: HttpClient) { }

  getBoards(): Observable<Board[]> {
    return this.http.get<Board[]>(`${this.apiUrl}/boards`);
  }

  // getPosts(): Observable<Post[]>{
  //   return this.http.get<Post[]>(`${this.apiUrl}/posts`);
  // }
}
