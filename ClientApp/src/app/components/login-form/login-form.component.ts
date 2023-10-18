import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit{
  username: string;
  password: string;
  @Output() onCreateAccount: EventEmitter<Event> = new EventEmitter();
  constructor(){}

  ngOnInit(): void {
    
  }

  // test(){
  //   console.log("clicked here")
  // }
  signupButtonClickHandler(): void{
    this.onCreateAccount.emit();
  }
}
