import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from '../models/Board';
import { Post } from '../models/Post';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ForumDataService {
  //json-server server
  // private apiUrl = 'http://localhost:5000';
  //local dev server
  private apiUrl = 'https://localhost:7061/api';

  constructor(private http: HttpClient) { }

  getBoards(): Observable<Board[]> {
    return this.http.get<Board[]>(`${this.apiUrl}/Boards`);
  }

  getSingleBoardWithPosts(id: number): Observable<Board>{
    return this.http.get<Board>(`${this.apiUrl}/Boards/${id}`);
  }

  // getBoardPosts(id: number): Observable<Post[]>{
  //   return this.http.get<Post[]>(`${this.apiUrl}/Posts/boardposts/${id}`);
  // }

  getSinglePost(id: number): Observable<Post>{
    return this.http.get<Post>(`${this.apiUrl}/Posts/${id}`);
  }
}
