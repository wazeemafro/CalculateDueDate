import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:44388/api/DueDate';
  constructor(private http: HttpClient) { }

  sendData(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/CalculateEndDateAsync`, data);
  }
}