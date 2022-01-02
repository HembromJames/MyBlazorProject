using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Pages
{
    public class DatabindingDemoBase : ComponentBase
    {
        public string Name { get; set; } = "Jim";
        public string Gender { get; set; } = "Male";
        public string Colour { get; set; } = "background-color:white";
        public string Description { get; set; } =string.Empty;
    }
}
