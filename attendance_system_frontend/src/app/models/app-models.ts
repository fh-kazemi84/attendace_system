export interface Employee {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    hireDate: Date;
    position: number;
    salary: number;
    gender: number;
    addressDTO: Address;
    departmentId: number;
    userInfoDTO: UserInfo;
    attendanceRecordDTOs: AttendanceRecord[];
}

export interface Address {
    id: number;
    street: string;
    city: string;
    state: string;
    postalCode: string;
    country: string;
}

export interface UserInfo {
    id: number;
    username: string;
    passwordHash: string;
    role: number;
}

export interface AttendanceRecord {
    id: number;
    date: Date;
    checkInTime?: Date;
    checkOutTime?: Date;
    status: number;
}

export interface Department {
    id: number;
    name: string;
    description: string;
}

export enum Position {
    Manager = 0,
    Senior = 1,
    Junior = 2,
    Office_worker = 3
}

export enum Gender {
    Male = 0,
    Female = 1
}

export enum UserRole {
    Admin = 0,
    Employee = 1
}

export enum AttendanceStatus {
    Present = "Present",
    Absent = "Absent",
    Leave = "Leave",
    HalfDay = "HalfDay",
    Late = "Late",
    EarlyLeave = "EarlyLeave",
}