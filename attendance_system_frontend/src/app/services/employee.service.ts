import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

import { Employee, Address, UserInfo, AttendanceRecord } from '../models/app-models';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  defaultAddress: Address = {
    id: 0,
    street: '',
    city: '',
    state: '',
    postalCode: '',
    country: ''
  };

  defaultUserInfo: UserInfo = {
    id: 0,
    username: '',
    passwordHash: '',
    role: -1
  };

  defaultEmployee: Employee = {
    id: 0,
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    hireDate: new Date(),
    position: -1,
    salary: 0,
    gender: -1,
    departmentId: -1,
    addressDTO: { ...this.defaultAddress },
    userInfoDTO: { ...this.defaultUserInfo },
    attendanceRecordDTOs: []
  };

  newEmployee: Employee = { ...this.defaultEmployee };

  private apiUrl = "https://localhost:7279/api/Employee";

  constructor(private http: HttpClient) { }

  public getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.apiUrl}/employees`);
  }

  public getEmployeeById(id: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.apiUrl}/employees/${id}`);
  }

  public addEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(`${this.apiUrl}/add`, employee);
  }

  public deleteEmployee(id: number): Observable<Employee> {
    return this.http.delete<Employee>(`${this.apiUrl}/delete/${id}`, { responseType: 'text' as 'json' });
  }

  public updateEmployee(id: number, employee: Employee): Observable<Employee> {
    return this.http.put<Employee>(`${this.apiUrl}/update?id=${id}`, employee);
  }

  public getAttendanceRecords(employeeId: number): Observable<AttendanceRecord[]> {
    return this.http.get<AttendanceRecord[]>(`${this.apiUrl}/${employeeId}/attendances`);
  }

  public deleteAttendanceRecord(employeeId: number, attendancerRecordId: number): Observable<AttendanceRecord> {
    return this.http.delete<AttendanceRecord>(`${this.apiUrl}/${employeeId}/delete-attendance/${attendancerRecordId}`, { responseType: 'text' as 'json' });
  }
}