import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from '../interfaces/IUser';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { IGender } from '../interfaces/IGender';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = "users"

  constructor(private httpClient: HttpClient) { }

  public getUsers(): Observable<IUser[]> {
    return this.httpClient.get<IUser[]>(`${environment.apiUrl}/${this.url}`);
  }

  public getUser(userId: string): Observable<IUser> {
    return this.httpClient.get<IUser>(`${environment.apiUrl}/${this.url}/${userId}`);
  }

  public addUser(user: IUser): Observable<IUser> {
    return this.httpClient.post<IUser>(`${environment.apiUrl}/${this.url}`, user);
  }

  public editUser(user: IUser): Observable<IUser> {
    return this.httpClient.put<IUser>(`${environment.apiUrl}/${this.url}/${user.id}`, user);
  }

  public generateReport(): Observable<HttpResponse<Blob>> {
    return this.httpClient.get(`${environment.apiUrl}/${this.url}/generate`, { observe: 'response', responseType: 'blob' });
  }

  public GetGenderEnum(): Observable<IGender[]> {
    return this.httpClient.get<IGender[]>(`${environment.apiUrl}/${this.url}/getGenderEnum`);
}
}
