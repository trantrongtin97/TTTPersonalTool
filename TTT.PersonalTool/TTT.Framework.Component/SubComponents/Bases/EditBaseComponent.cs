using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace TTT.Framework.SubComponents
{
    public class EditBaseComponent : ComponentBase
    {
        [Parameter]
        public bool ReadOnly { get; set; } = false;

        protected bool ShowConfirmation { get; set; }

        [Parameter]
        public string ConfirmationTitle { get; set; } = "Edit";

        [Parameter, AllowNull]
        public string MsgBtnCancel { get; set; } = "Cancel";

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
