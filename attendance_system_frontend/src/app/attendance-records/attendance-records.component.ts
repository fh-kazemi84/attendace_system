import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';

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
  newRecord: AttendanceRecord;

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
          date: new Date(record.date).toLocaleDateString('en-CA'),
          checkInTime: record.checkInTime ? new Date(record.checkInTime).toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' }) : null,
          checkOutTime: record.checkOutTime ? new Date(record.checkOutTime).toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' }) : null,
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

  public onDeleteRecord(attendancerRecordId) {
    if (window.confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteAttendanceRecord(this.selectedEmployeeId, attendancerRecordId).subscribe({
        next: () => {
          this.onLoadAttendanceRecords();
        }
      });
    }
  }

  public onEditMode(attendancerRecordId) {
    this.employeeService.getAttendanceRecordById(this.selectedEmployeeId, attendancerRecordId)
      .subscribe((record) => {
        this.selectedAttendanceRecord = record;

        this.editedRecord = {
          ...this.selectedAttendanceRecord,
          date: new Date(this.selectedAttendanceRecord.date).toLocaleDateString('en-CA'),
          checkInTime: record.checkInTime ? new Date(record.checkInTime).toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' }) : null,
          checkOutTime: record.checkOutTime ? new Date(record.checkOutTime).toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' }) : null,
        }
        this.editMode = true;
      });
  }

  public onEditSubmit() {
    if (this.editMode) {
      if (this.selectedAttendanceRecord.checkInTime &&
        this.selectedAttendanceRecord.checkOutTime &&
        this.selectedAttendanceRecord.checkOutTime < this.selectedAttendanceRecord.checkInTime) {
        alert('CheckOut Time is smaller than checkIn time.\n Try again');
      } else {
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
            alert('Failed to update Attendance Record!');
          }
        });
      }
    }
  }

  public onUpdatedRecord() {
    const date = new Date(this.selectedAttendanceRecord.date).toLocaleDateString('en-CA');
    const checkInTime = `${date}T${this.selectedAttendanceRecord.checkInTime}:00Z`;
    const checkOutTime = `${date}T${this.selectedAttendanceRecord.checkOutTime}:00Z`;

    this.updatedAttendanceRecord = {
      ...this.selectedAttendanceRecord,
      checkInTime: new Date(checkInTime),
      checkOutTime: new Date(checkOutTime)
    };
  }

  public onAddMode() {
    this.newRecord = {
      id: 0,
      date: new Date(),
      status: -1
    };

    this.addMode = true;
  }

  public onAddSubmit(recordAddMode: NgForm) {
    if (this.addMode) {
      if (recordAddMode.invalid ||
        this.newRecord.status == -1
      ) {
        alert('Please fill out all fields.');
      }
      else if (this.newRecord.checkInTime &&
        this.newRecord.checkOutTime &&
        this.newRecord.checkOutTime < this.newRecord.checkInTime) {
        alert('CheckOut Time is smaller than checkIn time.\n Try again');
      }
      else {
        this.employeeService.findAttendanceRecordByEmployeeIdAndDate(
          this.selectedEmployeeId,
          this.newRecord.date
        ).subscribe({
          next: (exists) => {
            console.log(exists);
            if (!exists) {
              alert('Please select another date.\nThere is this date.')
            }
            else {
              this.onAddedRecord();
              this.employeeService.addAttendaceRecord(
                this.selectedEmployeeId,
                this.updatedAttendanceRecord
              ).subscribe({
                next: (record) => {
                  alert('Attendance Record added successfully!');
                  this.onLoadAttendanceRecords();
                  this.addMode = false;
                },
                error: (err) => {
                  alert('Failed to add Attendance Record!');
                }
              });
            }
          }
        })
      }
    }
  }

  public onAddedRecord() {
    const date = new Date(this.newRecord.date).toISOString().split('T')[0];
    const checkInTime = `${date}T${this.newRecord.checkInTime}:00Z`;
    const checkOutTime = `${date}T${this.newRecord.checkOutTime}:00Z`;

    this.updatedAttendanceRecord = {
      ...this.newRecord,
      date: new Date(date),
      checkInTime: new Date(checkInTime),
      checkOutTime: new Date(checkOutTime),
      status: this.newRecord.status
    }
  }

  public onCancelForm() {
    if (window.confirm('Are you sure you want to cancel?')) {
      this.onLoadAttendanceRecords();
      this.editMode = false;
      this.addMode = false;
    }
  }
}
