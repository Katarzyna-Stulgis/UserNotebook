import { IUser } from './../../interfaces/IUser';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IGender } from 'src/app/interfaces/IGender';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  @Output() usersUpdated = new EventEmitter<IUser>();
  private routeSub: Subscription = {} as Subscription;

  userId: string = "";
  title: string = "";

  firstNameFormControl = new FormControl('', [Validators.required]);
  lastNameFormControl = new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z]{1,150}$')]);
  birthDateFormControl = new FormControl('', [Validators.required, this.dateOfBirthValidator.bind(this)]);
  genderFormControl = new FormControl('', [Validators.required]);
  phoneFormControl = new FormControl('', [Validators.pattern('^[0-9]{9}$')]);
  positionFormControl = new FormControl('', [Validators.pattern('^[a-zA-Z]*$')]);
  shoeSizeFormControl = new FormControl('', [Validators.pattern('^[0-9]*$')]);

  isValidForm: boolean = false;
  user: IUser = {} as IUser;
  genders: IGender[] = [];

  constructor(
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      this.userId = params['id'];
    });

    if (this.userId != null) {
      this.title = "Edycja użytkownika";
      this.userService
        .getUser(this.userId)
        .subscribe((result: IUser) => {
          this.user = result;
        })
    }
    else {
      this.title = "Dodawanie użytkownika";
    }
    this.getGender();
  }

  getGender() {
    this.userService.GetGenderEnum()
      .subscribe(data => {
        this.genders = data;
        if (this.user.gender == null) {
          this.user.gender = this.genders[0].Value;
        }
      })
  }

  Save() {
    this.isValidForm = this.firstNameFormControl.valid
      && this.lastNameFormControl.valid
      && this.birthDateFormControl.valid
      && this.genderFormControl.valid
      && this.phoneFormControl.valid
      && this.positionFormControl.valid
      && this.shoeSizeFormControl.valid;
    if (this.isValidForm) {
      if (this.userId != null) {
        this.userService
          .editUser(this.user)
          .subscribe((users: IUser) => {
            this.usersUpdated.emit(users);
            this.router.navigate(['/']);
          })
      }
      else {
        this.userService
          .addUser(this.user)
          .subscribe((users: IUser) => {
            this.usersUpdated.emit(users);
            this.router.navigate(['/']);
          })
      }
    }
  }

  dateOfBirthValidator(control: FormControl) {
    const birthDate = new Date(control.value);
    const currentDate = new Date();

    if (birthDate >= currentDate) {
      return { futureDate: true };
    }
    return null;
  }
}
