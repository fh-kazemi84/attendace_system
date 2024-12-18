import { Component, OnInit } from '@angular/core';
import { NavigationEnd, RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';
import { filter } from 'rxjs';

import { EmployeeService } from '../services/employee.service';
import { Department, Employee, Gender, Position, UserRole } from '../models/app-models';
import { FormsModule, NgForm } from '@angular/forms';
import { DepartmentService } from '../services/department.service';


@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent implements OnInit {

  employeeList: Employee[];
  departments: Department[];

  detailsMode: boolean = false;
  addMode: boolean = false;

  newEmployee: Employee;

  rePassword: string = '';

  positions = Object.keys(Position).filter(key => isNaN(Number(key)));
  positionsValue = Object.values(Position).filter(value => !isNaN(Number(value)));

  genders = Object.keys(Gender).filter(key => isNaN(Number(key)));
  gendersValue = Object.values(Gender).filter(value => !isNaN(Number(value)));

  userRoles = Object.keys(UserRole).filter(key => isNaN(Number(key)));
  userRolesValue = Object.values(UserRole).filter(value => !isNaN(Number(value)));

  constructor(private router: Router,
    private employeeService: EmployeeService,
    private departmentService: DepartmentService) { }


  ngOnInit(): void {
    this.OnLoadEmployees();

    this.router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe(() => {
      if (this.router.url === '/admin/employees') {
        this.OnLoadEmployees();
      }
    });

    this.newEmployee = this.employeeService.defaultEmployee;

    this.departmentService.getDepartments().subscribe((departments) => {
      this.departments = departments;
    });
  }

  OnLoadEmployees() {
    this.employeeService.getEmployees().subscribe((employee) => {
      this.employeeList = employee;
    });
    this.detailsMode = false;
    this.addMode = false;
  }

  public OnEmployeeDetails(employeeId: number) {
    this.router.navigate(['/admin/employees/details', employeeId]);
    this.detailsMode = true;
  }

  switchToAddMode() {
    this.addMode = true;
  }

  switchToListMode() {
    this.addMode = false;
  }

  onSubmit(employeeForm: NgForm) {
    if (employeeForm.invalid ||
      this.newEmployee.position === -1 ||
      this.newEmployee.gender === -1 ||
      this.newEmployee.departmentId === -1 ||
      this.newEmployee.userInfoDTO.role === -1) {
      alert('Please fill out all required fields.');
    }
    else {
      if (this.newEmployee.userInfoDTO.passwordHash != this.rePassword) {
        alert('Password do not match.\nTry again.');
      }
      else {
        this.employeeService.addEmployee(this.newEmployee).subscribe({
          next: (employee) => {
            alert('Employee added successfully!');
            this.OnLoadEmployees();
            this.OnResetForm(employeeForm);
            this.switchToListMode();
          },
          error: (err) => {
            alert('Failed to add employee.');
          }
        });
      }
    }
  }

  OnResetForm(employeeForm: NgForm) {
    employeeForm.resetForm();
    this.newEmployee = { ...this.employeeService.newEmployee };
    this.newEmployee.addressDTO = { ...this.employeeService.defaultAddress };
    this.newEmployee.userInfoDTO = { ...this.employeeService.defaultUserInfo };
  }

  OnConfirmResetForm(employeeForm: NgForm) {
    if (window.confirm('Are you sure you want to clear the form?')) {
      this.OnResetForm(employeeForm);
    }
  }
}
