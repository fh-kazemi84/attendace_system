import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';

import { Department } from '../models/app-models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService implements OnInit {

  private apiUrl = "https://localhost:7279/api/Department";

  departments: Department[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    //this.loadDepartments();
    this.getDepartments();
  }

  public loadDepartments(): void {
    this.getDepartments().subscribe({
      next: (departments) => {
        this.departments = departments;
      },
      error: (err) => {
        console.error("Error loading departments:", err);
      }
    });
  }

  public getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.apiUrl}/departments`);
  }

  public getDepartmentById(id: number): Department | undefined {
    return this.departments.find((department) => {
      department.id === id
    });
  }

  public deleteDepartment(id: number): Observable<Department> {
    return this.http.delete<Department>(`${this.apiUrl}/delete/${id}`, { responseType: 'text' as 'json' });
  }

  public updateDepartment(id: number, department: Department): Observable<Department> {
    return this.http.put<Department>(`${this.apiUrl}/update?id=${id}`, department);
  }
}
