using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorTutorial.Components
{
    public class ConfirmBase : ComponentBase
    {
        public bool ShowConfirmation { get; set; } = false;
        [Parameter]
        public string ConfirmationTitle { get; set; } = "Delete Confirmation";
        [Parameter]
        public string ConfirmationMessage { get; set; } = "Are you sure you want to delete?";
        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }
        public void Show()
        {
            ShowConfirmation = true;
        }
        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}
