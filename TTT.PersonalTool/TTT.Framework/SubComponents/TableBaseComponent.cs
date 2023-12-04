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
    }
}
