import { HttpClient } from '@angular/common/http';
import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
import{Employee} from '../../Model/Employee';


@Injectable({
    providedIn:'root'
})
export class EmployeeService{
    private apiURL = 'http://localhost:63708/api/employees';
    constructor(private http:HttpClient)
    { }
    
    getAll() : Observable<Employee[]>{
        return this.http.get<Employee[]>(`${this.apiURL}/GetAll`);
    }

    getById(id:number) : Observable<Employee>{
        return this.http.get<Employee>(`${this.apiURL}/GetById/${id}`);
    }

    create(employee : Employee) : Observable<Employee>{
        return this.http.post<Employee>(`${this.apiURL}/CreateEmployee`, employee);
    }

    update(id:number, emp: Employee) : Observable<Employee>{
        return this.http.put<Employee>(`${this.apiURL}/UpdateEmployee?id=${id}`, emp);
    }

    deleteEmp(id:number) : Observable<Employee>{
        return this.http.delete<Employee>(`${this.apiURL}/DeleteEmployee/${id}`);
    }

}
