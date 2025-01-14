import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Department } from '../models/app-models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  private apiUrl = "https://localhost:7279/api/Department";

  constructor(private http: HttpClient) { }

  public getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.apiUrl}/departments`);
  }

  public getDepartmentById(id: number): Observable<Department> {
    return this.http.get<Department>(`${this.apiUrl}/departments/${id}`);
  }

  public addDepartment(department: Department): Observable<Department> {
    return this.http.post<Department>(`${this.apiUrl}/add`, department);
  }

  public deleteDepartment(id: number): Observable<Department> {
    return this.http.delete<Department>(`${this.apiUrl}/delete/${id}`, { responseType: 'text' as 'json' });
  }

  public updateDepartment(id: number, department: Department): Observable<Department> {
    return this.http.put<Department>(`${this.apiUrl}/update?id=${id}`, department);
  }
}
