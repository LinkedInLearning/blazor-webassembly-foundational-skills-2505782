using Microsoft.AspNetCore.Components;

namespace MyBlazorShopHosted.Web.BlazorWasm.Client.Components
{
    public abstract class PageInfoModel : ComponentBase
    {
        [Parameter]
        public virtual string? Title { get; set; }

        [Parameter]
        public virtual string? Description { get; set; }
    }
}
