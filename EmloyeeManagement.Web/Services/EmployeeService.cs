using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var response = await httpClient.GetFromJsonAsync<Employee[]>("api/employees");
            return response;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var response = await httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");
            return response;
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            var response = await httpClient.PutAsJsonAsync<Employee>("api/employees", updatedEmployee);
            if (response.IsSuccessStatusCode)
            {
                //var content = await response.Content.ReadAsStringAsync();
                var emp = JsonConvert.DeserializeObject<Employee>(await response.Content.ReadAsStringAsync());
                Employee returnedEmployee = new Employee()
                {
                    EmployeeId = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Email = emp.Email,
                    DateOfBirth = emp.DateOfBirth,
                    Gender = emp.Gender,
                    DepartmentId = emp.DepartmentId,
                    PhotoPath = emp.PhotoPath,
                    Department = emp.Department
                };
                return returnedEmployee;
            }
            else
            {
                return null;
            }
        }

        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            var response = await httpClient.PostAsJsonAsync<Employee>("api/employees", newEmployee);
            if (response.IsSuccessStatusCode)
            {
                //var content = await response.Content.ReadAsStringAsync();
                var emp = JsonConvert.DeserializeObject<Employee>(await response.Content.ReadAsStringAsync());
                Employee returnedEmployee = new Employee()
                {
                    EmployeeId = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Email = emp.Email,
                    DateOfBirth = emp.DateOfBirth,
                    Gender = emp.Gender,
                    DepartmentId = emp.DepartmentId,
                    PhotoPath = emp.PhotoPath,
                    Department = emp.Department
                };
                return returnedEmployee;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteEmployee(int id)
        {
            var response = await httpClient.DeleteAsync($"api/employees/{id}");
        }
    }
}
