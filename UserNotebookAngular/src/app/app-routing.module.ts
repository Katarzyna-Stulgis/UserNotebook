import { UsersListComponent } from './components/users-list/users-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditUserComponent } from './components/edit-user/edit-user.component';

const routes: Routes = [
  { path: '', component: UsersListComponent },
  { path: 'add-user', component: EditUserComponent },
  { path: 'users/:id/edit-user', component: EditUserComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
