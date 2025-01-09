import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { EmployeeService } from '../services/employee.service';
import { AttendanceRecord, AttendanceStatus } from '../models/app-models';

@Component({
  selector: 'app-attendance-records',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './attendance-records.component.html',
  styleUrl: './attendance-records.component.css'
})
export class AttendanceRecordsComponent implements OnInit {

  selectedEmployeeId: number;
  //attendanceRecords: any[];
  groupedAttendanceRecords: { month: string; records: any[] }[];

  editMode: boolean = false;
  addMode: boolean = false;

  selectedAttendanceRecord: AttendanceRecord;
  editedRecord: any;
  updatedAttendanceRecord: AttendanceRecord;

  attendanceStatuses = Object.keys(AttendanceStatus).filter(key => isNaN(Number(key)));
  attendanceStatusValue = Object.values(AttendanceStatus).filter(value => !isNaN(Number(value)));

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
      "Present",
      "Absent",
      "Leave",
      "HalfDay",
      "Late",
      "EarlyLeave"
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

  public onEditMode(attendancerRecordId) {
    this.employeeService.getAttendanceRecordById(this.selectedEmployeeId, attendancerRecordId)
      .subscribe((record) => {
        this.selectedAttendanceRecord = { ...record };

        this.editedRecord = {
          date: new Date(this.selectedAttendanceRecord.date).toISOString().split('T')[0],
          checkInTime: this.selectedAttendanceRecord.checkInTime ? new Date(this.selectedAttendanceRecord.checkInTime).toTimeString().split(' ')[0] : null,
          checkOutTime: this.selectedAttendanceRecord.checkOutTime ? new Date(this.selectedAttendanceRecord.checkOutTime).toTimeString().split(' ')[0] : null,
          status: this.selectedAttendanceRecord.status
        }

        this.editMode = true;
      });
  }

  public onAddMode() {
    this.addMode = true;
  }

  public onDeleteRecord(attendancerRecordId) {
    if (window.confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteAttendanceRecord(this.selectedEmployeeId, attendancerRecordId).subscribe({
        next: () => {
          this.onLoadAttendanceRecords();
        }
      });
    }
  }

  public onSubmit() {
    if (this.editMode) {
      this.onUpdatedRecord();
      this.employeeService.updateAttendanceRecord(
        this.selectedEmployeeId,
        this.updatedAttendanceRecord.id,
        this.updatedAttendanceRecord
      ).subscribe({
        next: (record) => {
          alert('Attendance Record updated successfully!');
          this.onLoadAttendanceRecords();
          this.editMode = false;
        },
        error: (err) => {
          alert('Failed to update employee!');
        }
      });
    }
    else if (this.addMode) {

    }
  }

  public onUpdatedRecord() {
    const checkInTime = new Date(`${this.editedRecord.date}T${this.editedRecord.checkInTime}Z`);
    const checkOutTime = new Date(`${this.editedRecord.date}T${this.editedRecord.checkOutTime}Z`);
    const status = this.editedRecord.status;
    this.updatedAttendanceRecord = {
      ...this.selectedAttendanceRecord,
      date: new Date(this.editedRecord.date),
      checkInTime: checkInTime,
      checkOutTime: checkOutTime,
      status: status
    };
  }

  public onCancelForm() {
    if (window.confirm('Are you sure you want to cancel?')) {
      this.onLoadAttendanceRecords();
      this.editMode = false;
    }
  }
}
