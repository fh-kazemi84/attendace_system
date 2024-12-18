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
}
