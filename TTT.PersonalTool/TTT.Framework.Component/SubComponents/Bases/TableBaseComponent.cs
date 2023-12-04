using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace TTT.Framework.SubComponents
{
    public class TableBaseComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<int> ActionDelete { get; set; }

        [Parameter]
        public EventCallback<object> ActionEdit { get; set; }


        [Parameter, AllowNull]
        public string MsgBtnCancel { get; set; } = "Cancel";

        [Parameter, AllowNull]
        public string MsgBtnDelete { get; set; } = "Delete";

        [Parameter, AllowNull]
        public string MsgBtnSave { get; set; } = "Save";
        [Parameter, AllowNull]
        public string MsgBtnEdit { get; set; } = "Edit";


        [Parameter, AllowNull]
        public string TitleConfirmDelete { get; set; } = "Delete !";

        [Parameter, AllowNull]
        public string MsgConfirmDelete { get; set; } = "Are you sure ?";

        [Parameter, AllowNull]
        public string TitleConfirmSave { get; set; } = "Save !";

        [Parameter, AllowNull]
        public string MsgConfirmSave { get; set; } = "Are you sure ?";
    }
}
