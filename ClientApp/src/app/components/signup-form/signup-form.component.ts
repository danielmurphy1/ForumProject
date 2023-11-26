import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent implements OnInit{
  username: string;
  email: string;
  password: string;
  @Output() onSignupSave: EventEmitter<Event> = new EventEmitter();
  errorMessage: string;
  isLoading: boolean = false;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    
  }
  saveButtonClickHandler(form: NgForm): void{
    this.isLoading = true;
    form.value.createdAt = new Date();
    
    this.authService.addNewUser(form.value)
    .subscribe(
      { 
        next: () => {
          this.isLoading = false;
          this.onSignupSave.emit();
        },
        error: (error) => {
          this.isLoading = false;
          this.errorMessage = error.error.message;
        }
      }
    );
  }
}
