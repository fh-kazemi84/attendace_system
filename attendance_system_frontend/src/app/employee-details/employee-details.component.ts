import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { EmployeeService } from '../services/employee.service';
import { DepartmentService } from '../services/department.service';
import { Department, Employee } from '../models/app-models';

@Component({
  selector: 'app-employee-details',
  standalone: true,
  imports: [],
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.css'
})
export class EmployeeDetailsComponent implements OnInit {

  selectedEmployee: Employee;
  selectedEmployeeId: number;

  position: string;
  gender: string;
  userRole: string;
  department: string;

  departments: Department[];

  constructor(private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService,
    private departmentService: DepartmentService) { }

  ngOnInit(): void {
    this.departmentService.getDepartments().subscribe((departments) => {
      this.departments = departments;
    });

    this.route.params.subscribe((params: Params) => {
      this.selectedEmployeeId = +params['id'];

      if (this.selectedEmployeeId) {
        this.employeeService.getEmployeeById(this.selectedEmployeeId).subscribe((employee) => {
          this.selectedEmployee = employee;
          this.setPosition();
          this.setGender();
          this.setUserRole();
          this.setDepartment(this.selectedEmployee.departmentId);
        });
      }
    });
  }

  public setPosition() {
    const positions = [
      "Manager",
      "Senior",
      "Junior",
      "Office_worker",
    ];
    this.position = positions[this.selectedEmployee.position] || "Unknown";
  }

  public setGender() {
    const genders = [
      "Male",
      "Female",
    ];
    this.gender = genders[this.selectedEmployee.gender] || "Unknown";
  }

  public setUserRole() {
    const userRoles = [
      "Admin",
      "Employee",
    ];
    this.userRole = userRoles[this.selectedEmployee.userInfoDTO.role] || "Unknown";
  }

  public setDepartment(id: number) {
    this.department = this.departments.find(department => department.id === id)?.name || "Unknown";
  }

  public onEmployeeList() {
    this.router.navigate(['/admin/employees']);
  }

  public onUsernameInfo(employeeId: number) {
    this.router.navigate(['/admin/employees/username-info', employeeId]);
  }

  public onEmployeeAttendance(employeeId: number) {
    this.router.navigate(['/admin/employees/attendance-records', employeeId]);
  }

  public OnDeleteEmployee(id: number) {
    this.employeeService.deleteEmployee(id).subscribe({
      next: () => {
        alert('Employee deleted successfully!');
        this.onEmployeeList();
      },
      error: (err) => {
        console.log('Delete error: ',err);
        alert('Failed to delete employee.');
      }
    });
  }
}

