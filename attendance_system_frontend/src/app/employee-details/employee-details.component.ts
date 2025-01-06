import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';

import { EmployeeService } from '../services/employee.service';
import { DepartmentService } from '../services/department.service';
import { Department, Employee, Gender, Position, UserRole } from '../models/app-models';

@Component({
  selector: 'app-employee-details',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.css'
})
export class EmployeeDetailsComponent implements OnInit {

  selectedEmployee: Employee;
  selectedEmployeeId: number;

  updatedEmployee: Employee;

  position: string;
  gender: string;
  userRole: string;
  department: string;
  currentHireDate: string;

  departments: Department[];

  editMode: boolean = false;

  genders = Object.keys(Gender).filter(key => isNaN(Number(key)));
  gendersValue = Object.values(Gender).filter(value => !isNaN(Number(value)));

  positions = Object.keys(Position).filter(key => isNaN(Number(key)));
  positionsValue = Object.values(Position).filter(value => !isNaN(Number(value)));

  userRoles = Object.keys(UserRole).filter(key => isNaN(Number(key)));
  userRolesValue = Object.values(UserRole).filter(value => !isNaN(Number(value)));

  constructor(private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService,
    private departmentService: DepartmentService) { }

  ngOnInit(): void {
    this.departmentService.getDepartments().subscribe((departments) => {
      this.departments = departments;
    });

    this.onSelectedEmployee();
  }

  public onSelectedEmployee() {
    this.route.params.subscribe((params: Params) => {
      this.selectedEmployeeId = +params['id'];

      if (this.selectedEmployeeId) {
        this.employeeService.getEmployeeById(this.selectedEmployeeId).subscribe((employee) => {
          this.selectedEmployee = employee;
          this.currentHireDate = new Date(this.selectedEmployee.hireDate).toISOString().split('T')[0];
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
    if (window.confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(id).subscribe({
        next: () => {
          this.onEmployeeList();
        }
      });
    }
  }

  public onEditMode() {
    this.onUpdateEmployee();
    this.editMode = true;
  }

  public onSubmit(detailsEditMode: NgForm) {
    if (detailsEditMode.invalid) {
      alert('Please fill out all required fields.');
    }
    else {
      this.employeeService.updateEmployee(this.selectedEmployeeId, this.selectedEmployee).subscribe({
        next: (employee) => {
          alert('Employee updated successfully!');
          this.onSelectedEmployee();
          this.editMode = false;
        },
        error: (err) => {
          alert('Failed to update employee!');
        }
      });
    }
  }

  onCancelForm() {
    if (window.confirm('Are you sure you want to cancel?')) {
      this.onSelectedEmployee();
      this.editMode = false;
    }
  }

  onUpdateEmployee() {
    this.updatedEmployee = { ...this.selectedEmployee };
    this.updatedEmployee.gender = this.selectedEmployee.gender;
    this.updatedEmployee.position = this.selectedEmployee.position;
    this.updatedEmployee.userInfoDTO = { ...this.selectedEmployee.userInfoDTO };
    this.updatedEmployee.departmentId = this.selectedEmployee.departmentId;
    this.updatedEmployee.addressDTO = { ...this.selectedEmployee.addressDTO };
    this.updatedEmployee.attendanceRecordDTOs = { ...this.selectedEmployee.attendanceRecordDTOs };
  }

  onUpdateSelectedEmployee() {

    this.selectedEmployee = { ...this.updatedEmployee };
    this.selectedEmployee.gender = this.updatedEmployee.gender;
    this.selectedEmployee.position = this.updatedEmployee.position;
    this.selectedEmployee.userInfoDTO = { ...this.updatedEmployee.userInfoDTO };
    this.selectedEmployee.departmentId = this.updatedEmployee.departmentId;
    this.selectedEmployee.addressDTO = { ...this.updatedEmployee.addressDTO };
    this.selectedEmployee.attendanceRecordDTOs = { ...this.updatedEmployee.attendanceRecordDTOs };
  }
}

