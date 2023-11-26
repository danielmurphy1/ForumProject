import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from '../models/Board';
import { Post } from '../models/Post';
import { Reply } from '../models/Reply';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class ForumDataService {
  //json-server server
  // private apiUrl = 'http://localhost:5000';
  //local dev server
  private apiUrl = 'https://localhost:7061/api';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  getBoards(): Observable<Board[]> {
    return this.http.get<Board[]>(`${this.apiUrl}/Boards`);
  }

  getSingleBoardWithPosts(id: number): Observable<Board>{
    return this.http.get<Board>(`${this.apiUrl}/Boards/${id}`);
  }

  getSinglePostWithReplies(id: number): Observable<Post>{
    return this.http.get<Post>(`${this.apiUrl}/Posts/${id}`);
  }

  addNewPost(post: Post): Observable<Post>{
    return this.http.post<Post>(`${this.apiUrl}/Posts`, post, this.httpOptions);
  }

  addNewReply(reply: Reply): Observable<Reply>{
    return this.http.post<Reply>(`${this.apiUrl}/Replies`, reply, this.httpOptions);
  } 

  updatePostViewsOrReplies(post: Post, property: string): Observable<Post>{
    return this.http.put<Post>(`${this.apiUrl}/Posts/${post.id}/${property}`, post, this.httpOptions);
  }

  addNewUser(user: User): Observable<User>{
    return this.http.post<User>(`${this.apiUrl}/Users/signup`, user, this.httpOptions);
  }
}
