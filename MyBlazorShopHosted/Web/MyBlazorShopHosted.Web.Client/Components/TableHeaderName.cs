namespace MyBlazorShopHosted.Web.Client.Components
{
    public class TableHeaderName
    {
        public TableHeaderName(string name, int colspan = 1)
        {
            Name = name;
            Colspan = colspan;
        }

        public int Colspan { get; }

        public string Name { get;}
    }
}
