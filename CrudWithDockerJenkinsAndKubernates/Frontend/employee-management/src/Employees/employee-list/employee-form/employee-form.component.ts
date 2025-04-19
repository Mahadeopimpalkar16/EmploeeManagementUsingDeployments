import { Component } from '@angular/core';
import { Employee } from '../../Model/Employee';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.scss'], // change to .css if needed
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class EmployeeFormComponent {
  employee: Employee = {
    fullName: '',
    email: '',
    department: '',
    gender: '',
    dateOfJoining: new Date()
  };

  onSubmit() {
    console.log('Employee Submitted:', this.employee);
    // Later we’ll connect this to the API via service
  }
}
