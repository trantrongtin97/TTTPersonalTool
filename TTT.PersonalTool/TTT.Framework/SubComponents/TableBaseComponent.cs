using Microsoft.AspNetCore.Components;

namespace TTT.Framework.SubComponents
{
    public class TableBaseComponent : ComponentBase
    {
        [Parameter]
        public List<object> Datas { get; set; } = new List<object>();

        [Parameter]
        public string[] ShowColumns { get; set; } = { "Id" };

    }
}
