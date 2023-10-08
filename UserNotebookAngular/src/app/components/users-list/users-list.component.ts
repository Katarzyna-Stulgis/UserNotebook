import { IUser } from 'src/app/interfaces/IUser';
import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})

export class UsersListComponent implements OnInit {

  users: IUser[] = [];
  dataSource = new MatTableDataSource<IUser>();
  displayedColumns: string[] = ['firstName', 'lastName', 'birthDate', 'gender', 'phoneNumber', 'position', 'shoeSize'];

  constructor(
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService
      .getUsers()
      .subscribe((result: IUser[]) => {
        this.users = result;
        this.dataSource.data = this.users;
      });
  }
}