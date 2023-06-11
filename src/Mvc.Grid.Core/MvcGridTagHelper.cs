using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace NonFactors.Mvc.Grid.TagHelpers
{ 
    [HtmlTargetElement("mvcgrid")]
    public class MvcGridTagHelper : TagHelper
    {
        IUrlHelperFactory urlHelperFactory;

        public MvcGridTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        [HtmlAttributeName("asp-action")]
        public string? Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string? Controller { get; set; }

		[HtmlAttributeName("asp-showPageSizes")]
		public bool ShowPageSizes { get; set; }


		public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(Controller))
            {
                throw new ArgumentNullException("MvcGrid controller is undefined");
            }

            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            output.TagName = "div";  
            
            string? url = urlHelper.Action(Action, Controller);

            output.Attributes.Add("data-url", url);
            output.Attributes.Add("class", "mvc-grid");
            output.Attributes.Add("data-showPageSizes", ShowPageSizes.ToString().ToLower());

            base.Process(context, output);
        }

    }
}
