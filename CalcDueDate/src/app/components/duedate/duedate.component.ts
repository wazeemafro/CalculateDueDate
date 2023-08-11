import { Component } from '@angular/core';
import { ApiService } from '../../api.service';

@Component({
  selector: 'app-duedate',
  templateUrl: './duedate.component.html',
  styleUrls: ['./duedate.component.css']
})
export class DuedateComponent {
  selectedDate: string = "";
  inputText: number = 0;
  endDate : string = "";

  constructor(private apiService: ApiService) { }

  submitData() {
    const data = {
      StartDate: this.selectedDate,
      WorkingDays: this.inputText
    };

    this.apiService.sendData(data).subscribe(response => {
      console.log('API response:', response);
      this.endDate = response.replace(/T.*$/, "");
    });
  }
}
