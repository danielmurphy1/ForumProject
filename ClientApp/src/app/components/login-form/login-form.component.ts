import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit{
  username: string;
  password: string;
  errorMessage: string;
  isLoading: boolean = false;
  @Output() onCreateAccount: EventEmitter<Event> = new EventEmitter();

  constructor(private authService: AuthService, private route: Router){}

  ngOnInit(): void {
    
  }

  // test(){
  //   console.log("clicked here")
  // }
  signupButtonClickHandler(): void{
    this.onCreateAccount.emit();
  }

  loginButtonClickHandler(form: NgForm): void {
    this.isLoading = true;
    console.log(form.value);
    this.authService.login(form.value) 
    .subscribe(
      { 
        next: (resUser) => {
          console.log(resUser);
          this.authService.storeUserCredentials(resUser.token!, resUser.username, resUser.id!);
          this.errorMessage = '';
          this.route.navigate(['boards']);
          this.isLoading = false;
        }, 
        error: (err) => {
          this.errorMessage = err.error;
          this.isLoading = false;
        }
      } 
    );
  }
}
