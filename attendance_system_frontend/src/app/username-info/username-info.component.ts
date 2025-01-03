import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';

import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/app-models';

@Component({
  selector: 'app-username-info',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './username-info.component.html',
  styleUrl: './username-info.component.css'
})
export class UsernameInfoComponent implements OnInit {

  selectedEmployeeId: number;
  foundEmployee: Employee;
  showPassword: boolean = false;
  editMode: boolean = false;
  rePassword: string = '';

  constructor(private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.selectedEmployeeId = +params['id'];

      if (this.selectedEmployeeId) {
        this.employeeService.getEmployeeById(this.selectedEmployeeId).subscribe((employee) => {
          this.foundEmployee = employee;
        });
      }
    });
  }

  public onEmployeeDetails() {
    this.router.navigate(['/admin/employees/details', this.selectedEmployeeId]);
  }

  public onUsernameInfo() {
    if (window.confirm('Are you sure you want to cancel?')) {
      this.editMode = false;
    }
  }

  public togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  onEditMode() {
    this.editMode = true;
  }

  onSubmit(usernameEditForm: NgForm) {
    if (!this.foundEmployee.userInfoDTO.username ||
      !this.foundEmployee.userInfoDTO.passwordHash ||
      !this.rePassword) {
      alert('Please fill username field.');
    }
    else if (this.foundEmployee.userInfoDTO.passwordHash != this.rePassword) {
      alert('Password do not match.\nTry again.');
    }
    else {
      this.employeeService.updateEmployee(this.selectedEmployeeId, this.foundEmployee).subscribe({
        next: (employee) => {
          alert('Employee updated successfully!');
          this.onEmployeeDetails();
          this.editMode = false;
        },
        error: (err) => {
          alert('Failed to update employee!');
        }
      });
    }
  }

  public onDiesabledPaste($event: ClipboardEvent) {
    $event.preventDefault();
    alert('Paste Not Allowed!\nPlease retype password.');
  }
}
