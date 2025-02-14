import { Component, OnInit } from '@angular/core';
import { Department } from '../models/app-models';
import { Router } from '@angular/router';
import { DepartmentService } from '../services/department.service';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-department',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})
export class DepartmentComponent implements OnInit {

  departmentList: Department[];
  selectedDepartment: Department;
  newDepartment: Department;

  editMode: boolean = false;
  addMode: boolean = false;

  constructor(private router: Router,
    private departmentService: DepartmentService) { }

  ngOnInit(): void {
    this.onLoadDepartments();
  }

  onLoadDepartments() {
    this.departmentService.getDepartments().subscribe({
      next: (departments) => {
        this.departmentList = departments;
      },
      error: (err) => {
        alert('Failed to load Department List!')
      }
    });
  }

  public onEmployeeList() {
    this.router.navigate(['/admin/employees']);
  }

  onEditMode(departmentId) {
    this.departmentService.getDepartmentById(departmentId).subscribe((department) => {
      this.selectedDepartment = department;
      this.editMode = true;
    });
  }

  onEditSubmit(editModeForm: NgForm) {
    if (editModeForm.invalid) {
      alert('Please fill out all required fields.');
    }
    else {
      this.departmentService.updateDepartment(
        this.selectedDepartment.id,
        this.selectedDepartment
      ).subscribe({
        next: (department) => {
          alert('Department updated successfully!');
          this.onLoadDepartments();
          this.editMode = false;
        },
        error: (err) => {
          alert('Failed to update Department!');
        }
      });
    }
  }

  onCancelForm() {
    if (window.confirm('Are you sure you want to cancel?')) {
      this.editMode = false;
      this.addMode = false;
    }
  }

  onDeleteDepartment(departmentId) {
    if (window.confirm('Are you sure you want to delete this department?')) {
      this.departmentService.deleteDepartment(departmentId).subscribe({
        next: () => {
          this.onLoadDepartments();
        }
      });
    }
  }

  onAddMode() {
    this.newDepartment = {
      id: 0,
      name: '',
      description: ''
    };
    this.addMode = true;
  }

  onAddSubmit(addModeForm: NgForm) {
    if (addModeForm.invalid) {
      alert('Please fill out all required fields.');
    } else {
      this.departmentService.addDepartment(this.newDepartment).subscribe({
        next: (department) => {
          alert('Department added successfully!');
          this.onLoadDepartments();
          this.addMode = false;
        },
        error: (err) => {
          alert('Failed to add Department!');
        }
      });
    }
  }
}
