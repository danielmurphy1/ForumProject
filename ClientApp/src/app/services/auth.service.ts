import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject, of, tap } from 'rxjs';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // private apiUrl = 'https://localhost:7061/api';
  //live server
  private apiUrl = '/api';
  isAuthenticated = new BehaviorSubject<boolean>(false);
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { 
    this.isAuthenticated.next(this.isLoggedIn()); 
  }

  login(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/Users/login`, user, this.httpOptions);
  }

  addNewUser(user: User): Observable<User>{
    return this.http.post<User>(`${this.apiUrl}/Users/signup`, user, this.httpOptions);
  }
  
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    localStorage.removeItem('userId');
  }


  getToken(): string {
    return localStorage.getItem('token')!;
  }

  isLoggedIn(): boolean {
    return this.getToken() ? true : false;
  }

  storeUserCredentials(token: string, username: string, userId: number): void {
    localStorage.setItem('token', token);
    localStorage.setItem('username', username);
    localStorage.setItem('userId', userId.toString());
  }
}
