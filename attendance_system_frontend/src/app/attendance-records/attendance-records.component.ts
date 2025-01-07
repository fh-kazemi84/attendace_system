import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { EmployeeService } from '../services/employee.service';
import { AttendanceStatus } from '../models/app-models';

@Component({
  selector: 'app-attendance-records',
  standalone: true,
  imports: [],
  templateUrl: './attendance-records.component.html',
  styleUrl: './attendance-records.component.css'
})
export class AttendanceRecordsComponent implements OnInit {

  selectedEmployeeId: number;
  //attendanceRecords: any[];
  groupedAttendanceRecords: { month: string; records: any[] }[];

  editMode: boolean = false;
  addMode: boolean = false;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService) { }

  // ngOnInit(): void {
  //   this.route.params.subscribe((params: Params) => {
  //     this.selectedEmployeeId = +params['id'];

  //     if (this.selectedEmployeeId) {
  //       this.employeeService.getEmployeeById(this.selectedEmployeeId).subscribe((employee) => {
  //         this.attendanceRecords = employee.attendanceRecordDTOs.map(record => ({
  //           ...record,
  //           date: new Date(record.date).toISOString().split('T')[0],
  //           checkInTime: record.checkInTime ? new Date(record.checkInTime).toTimeString().split(' ')[0] : null,
  //           checkOutTime: record.checkOutTime ? new Date(record.checkOutTime).toTimeString().split(' ')[0] : null,
  //           status: this.setStatus(record.status)
  //         }));
  //       });
  //     };
  //   });
  // }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.selectedEmployeeId = +params['id'];
      this.onLoadAttendanceRecords();
    });
  }

  public onLoadAttendanceRecords() {
    if (this.selectedEmployeeId) {
      this.employeeService.getEmployeeById(this.selectedEmployeeId).subscribe((employee) => {
        const records = employee.attendanceRecordDTOs.map(record => ({
          ...record,
          date: new Date(record.date).toISOString().split('T')[0],
          checkInTime: record.checkInTime ? new Date(record.checkInTime).toTimeString().split(' ')[0] : null,
          checkOutTime: record.checkOutTime ? new Date(record.checkOutTime).toTimeString().split(' ')[0] : null,
          status: this.setStatus(record.status)
        }));

        this.groupedAttendanceRecords = this.groupByMonth(records);
      });
    }
  }

  public setStatus(status: number): string {
    const statuses = [
      AttendanceStatus.Present,
      AttendanceStatus.Absent,
      AttendanceStatus.Leave,
      AttendanceStatus.HalfDay,
      AttendanceStatus.Late,
      AttendanceStatus.EarlyLeave,
    ];
    return statuses[status] || "Unknown";
  }

  groupByMonth(records: any[]): { month: string; records: any[] }[] {
    const grouped: { month: string; records: any[] }[] = [];

    const sortedRecords = records.sort((a, b) => {
      const dateA = new Date(a.date);
      const dateB = new Date(b.date);
      return dateB.getTime() - dateA.getTime(); //decending sort
    });

    // records.forEach(record => {
    //   const date = new Date(record.date);
    //   const month = `${date.toLocaleString('default', { month: 'long' })} ${date.getFullYear()}`;

    //sorted year and then month
    sortedRecords.forEach(record => {
      const date = new Date(record.date);
      const month = `${date.toLocaleString('default', { month: 'long' })} ${date.getFullYear()}`;

      let monthGroup = grouped.find(group => group.month === month);
      if (!monthGroup) {
        monthGroup = { month, records: [] };
        grouped.push(monthGroup);
      }
      monthGroup.records.push(record);
    });

    return grouped;
  }

  public onEmployeeDetails() {
    this.router.navigate(['/admin/employees/details', this.selectedEmployeeId]);
  }

  public onEditMode() {
    this.editMode = true;
  }

  public onAddMode() {
    this.addMode = true;
  }

  public onDeleteRecord(selectedEmployeeId, attendancerRecordId) {
    if (window.confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteAttendanceRecord(selectedEmployeeId, attendancerRecordId).subscribe({
        next: () => {
          this.onLoadAttendanceRecords();
        }
      });
    }
  }
}
