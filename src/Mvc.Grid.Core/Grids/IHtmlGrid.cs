using Microsoft.AspNetCore.Html;
using System;

namespace NonFactors.Mvc.Grid
{
    public interface IHtmlGrid<T> : IHtmlAsyncContent
	{
        IGrid<T> Grid { get; }

        String PartialViewName { get; set; }
    }
}
