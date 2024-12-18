import { Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { EmployeeComponent } from './employee/employee.component';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';
import { AdminComponent } from './admin/admin.component';
import { UsernameInfoComponent } from './username-info/username-info.component';
import { AttendanceRecordsComponent } from './attendance-records/attendance-records.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    {
        path: 'admin', component: AdminComponent, children: [
            {
                path: 'employees', component: EmployeeComponent, children: [

                    { path: 'details/:id', component: EmployeeDetailsComponent },
                    { path: 'username-info/:id', component: UsernameInfoComponent },
                    { path: 'attendance-records/:id', component: AttendanceRecordsComponent },
                ]
            },
        ]
    },
];
