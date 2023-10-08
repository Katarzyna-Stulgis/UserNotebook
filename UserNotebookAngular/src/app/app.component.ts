import { Component } from '@angular/core';
import { UserService } from './services/user.service';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'UserNotebookAngular';

  constructor(
    private userService: UserService
  ) { }

  generateReport() {
    this.userService.generateReport().subscribe(
      (response) => {
        const blob = response.body as Blob;

        const fileName = response.headers.get('File-Name') || 'generated-report.xlsx';

        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        document.body.appendChild(a);
        a.style.display = 'none';
        a.href = url;
        a.download = fileName;
        a.click();
        window.URL.revokeObjectURL(url);
      },
      (error) => {
        console.error('Error generating report:', error);
      }
    );
  }

  
}
