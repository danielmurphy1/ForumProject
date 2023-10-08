import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { SignupFormComponent } from './components/signup-form/signup-form.component';
import { BoardsComponent } from './components/boards/boards.component';
import { PostsComponent } from './components/posts/posts.component';
import { NewPostFormComponent } from './components/new-post-form/new-post-form.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'boards', component: BoardsComponent },
  // { path: 'boards/College Football', component: HomeComponent }
  { path: 'boards/:id', component: PostsComponent },
  { path: '**', component: HomeComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    LoginFormComponent,
    SignupFormComponent,
    BoardsComponent,
    PostsComponent,
    NewPostFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
