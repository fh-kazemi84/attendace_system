import { Component, OnInit } from '@angular/core';
import { Department } from '../models/app-models';
import { Router } from '@angular/router';
import { DepartmentService } from '../services/department.service';

@Component({
  selector: 'app-department',
  standalone: true,
  imports: [],
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})
export class DepartmentComponent implements OnInit {

  departmentList: Department[];
  selectedDepartment: Department;

  editDepartmentMode: boolean = false;

  constructor(private router: Router,
    private departmentService: DepartmentService
  ) { }

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

  public onEditDepartment(departmentId) { }

  public onDeleteDepartment(departmentId) { }
}
