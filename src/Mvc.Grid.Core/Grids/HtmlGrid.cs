using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace NonFactors.Mvc.Grid
{
    public class HtmlGrid<T> : IHtmlGrid<T>
    {
        public IGrid<T> Grid { get; set; }
        public IHtmlHelper Html { get; set; }
        public String PartialViewName { get; set; }

        public HtmlGrid(IHtmlHelper html, IGrid<T> grid)
        {
            Html = html;
            Grid = grid;
            PartialViewName = "MvcGrid/_Grid";
            grid.ViewContext ??= html.ViewContext;
            grid.Query ??= grid.ViewContext.HttpContext.Request.Query;
        }

		public async ValueTask WriteToAsync(TextWriter writer)
		{
			var result = await Html.PartialAsync(PartialViewName, Grid);
			result.WriteTo(writer, HtmlEncoder.Default);
		}

		public void WriteTo(TextWriter writer, HtmlEncoder encoder)
		{
			Html.PartialAsync(PartialViewName, Grid).Result.WriteTo(writer, encoder);
		}
	}
}
