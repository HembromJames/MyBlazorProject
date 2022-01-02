using AutoMapper;
using BlazorTutorial.Components;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        private Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel editEmployeeModel { get; set; } =  new EditEmployeeModel();
        public List<Department> Departments { get; set; } = new List<Department>();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Inject]
        public IMapper mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public int MyProperty { get; set; }
        public string PageHeaderText { get; set; }
        public ConfirmBase DeleteConfirmation { get; set; }

        [Parameter]
        public string Id { get; set; }
        protected async override Task OnInitializedAsync()
        {
            int.TryParse(Id, out int employeeId);
            if(employeeId != 0)
            {
                PageHeaderText = "Edit Employee";
                Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {
                PageHeaderText = "Create Employee";
                Employee  = new Employee()
                {
                    DepartmentId = 1,
                    DateOfBirth = DateTime.Now,
                    PhotoPath = "/images/nophoto.jpg"
                };
            }
            Departments = (await DepartmentService.GetDepartments()).ToList();

            mapper.Map(Employee,editEmployeeModel);

            //editEmployeeModel.FirstName = Employee.FirstName;
            //editEmployeeModel.LastName = Employee.LastName;
            //editEmployeeModel.Email = Employee.Email;
            //editEmployeeModel.ConfirmEmail = Employee.Email;
            //editEmployeeModel.PhotoPath = Employee.PhotoPath;
            //editEmployeeModel.DateOfBirth = Employee.DateOfBirth;
            //editEmployeeModel.DepartmentId = Employee.DepartmentId;
            //editEmployeeModel.Department = Employee.Department;
        }
        protected async Task HandleValidSubmit()
        {
            mapper.Map(editEmployeeModel, Employee);
            Employee result = null;
            if(Employee.EmployeeId != 0)
            {
                result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                var department =  await DepartmentService.GetDepartment(Employee.DepartmentId);
                Employee.Department = department;
                result = await EmployeeService.CreateEmployee(Employee);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
        protected async Task Delete_Click()
        {
            DeleteConfirmation.Show();            
        }
        protected async Task ConfirmDelete_Click(bool value)
        {
            if(value)
            {
                await EmployeeService.DeleteEmployee(editEmployeeModel.EmployeeId);
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
