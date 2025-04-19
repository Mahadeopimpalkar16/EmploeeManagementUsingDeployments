import { Component, OnInit } from '@angular/core';
import { CommonModule} from '@angular/common';
import { Employee } from '../Model/Employee';
import { EmployeeService } from './employee-form/employee-services';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, FormsModule ],
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss'] // ✅ Corrected: style**Urls**, not styleUrl
})
export class EmployeeListComponent implements OnInit {

  employees: Employee[] = [];
  showCreateRow: boolean = false;
  editingEmployeeId: number | null = null;
  IsNewObject : boolean = false;
  currentEmployee : Employee = {} as Employee;
  employee: Employee = {
    id: 0,
    fullName: '',
    email: '',
    department: '',
    gender: '',
    dateOfJoining: new Date()
  };

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees(): void {
    this.employeeService.getAll().subscribe({
      next: (data: Employee[]) => this.employees = data,
      error: (err) => console.error('Error fetching employees', err)
    });
  }

  OnsaveClick(): void {
    if(this.IsNewObject){
        this.employeeService.create(this.currentEmployee).subscribe(updatedEmp => {
        this.employees.push(updatedEmp)
      });
      this.IsNewObject = false;
    }
    else
    {
      if(this.editingEmployeeId === null) return;
      this.employeeService.update(this.editingEmployeeId, this.currentEmployee).subscribe(updatedEmp => {
        const index = this.employees.findIndex(emp => emp.id === this.editingEmployeeId);
        if(index !== -1){
          this.employees[index] = updatedEmp;
        }
      });
    }
    this.OncancelClick();
  }
  
  OnEditClick(emp: Employee): void {
    this.currentEmployee = {...emp};
    this.editingEmployeeId = emp.id ?? null;
  }
  OnNewClick() : void{
    this.IsNewObject = true;
  }
  OnDeleteClick(id: number | undefined): void {
    if (id === undefined) return;
  
    this.employeeService.deleteEmp(id).subscribe({
      next: () => {
        this.employees = this.employees.filter(e => e.id !== id);
      },
      error: (err) => console.error('Error deleting employee', err)
    });
  }
  OncancelClick() : void{
    this.editingEmployeeId = null;
    this.IsNewObject = false;
    this.resetForm();
    this.getEmployees(); // to reset changes
  }
    resetForm(): void {
      this.currentEmployee = {
        fullName: '',
        email: '',
        department: '',
        gender: '',
        dateOfJoining: new Date()
      };
  }
}
