using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace TTT.Framework.SubComponents
{
    public class ConfirmBaseComponent : ComponentBase
    {
        protected bool ShowConfirmation { get; set; }

        [Parameter]
        public string ConfirmationTitle { get; set; } = "Confirmation !";

        [Parameter]
        public string ConfirmationMessage { get; set; } = "Are you sure ?";

        [Parameter, AllowNull]
        public string MsgBtnCancel { get; set; } = "Cancel";

        [Parameter, AllowNull]
        public string MsgBtnDelete { get; set; } = "Delete";

        [Parameter, AllowNull]
        public string MsgBtnSave { get; set; } = "Save";

        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}
