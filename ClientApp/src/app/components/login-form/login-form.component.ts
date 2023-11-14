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
    console.log(form.value);
    this.authService.login(form.value)
    // .subscribe(resUser => {
    //   console.log(resUser);
    // }, error => {
    //   console.log(error.error);
    //   this.errorMessage = error.error;
    // }); 
    .subscribe(
      { 
        next: (resUser) => {
          console.log(resUser);
          this.authService.storeToken(resUser.token!);
          this.errorMessage = '';
          this.route.navigate(['boards']);
        }, 
        error: (err) => {
          this.errorMessage = err.error;
        }
      } 
    )
  }
}
