import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { EmployeeService } from '../services/employee.service';
import { UserInfo } from '../models/app-models';

@Component({
  selector: 'app-username-info',
  standalone: true,
  imports: [],
  templateUrl: './username-info.component.html',
  styleUrl: './username-info.component.css'
})
export class UsernameInfoComponent implements OnInit {

  selectedEmployeeId: number;
  userInfo: UserInfo;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.selectedEmployeeId = +params['id'];

      if (this.selectedEmployeeId) {
        this.employeeService.getEmployeeById(this.selectedEmployeeId).subscribe((employee) => {
          this.userInfo = employee.userInfoDTO;
        });
      }
    });
  }

  public onEmployeeDetails() {
    this.router.navigate(['/admin/employees/details', this.selectedEmployeeId]);
  }
}
