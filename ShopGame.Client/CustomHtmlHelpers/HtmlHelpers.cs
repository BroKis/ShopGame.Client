using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ShopGame.Client.CustomHtmlHelpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent CreateEntityButton(this IHtmlHelper htmlHelper, string controller)
        {
            var link = new TagBuilder("a");
            link.Attributes["class"] = "btn btn-success";
            link.InnerHtml.AppendHtml("<i class=\"bi bi-plus-circle\"></i> Сreate");

            var urlHelperFactory = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var urlHelper = urlHelperFactory.GetUrlHelper(htmlHelper.ViewContext);

            var url = urlHelper.Action("Create", controller);

            link.Attributes["href"] = url;

            var div = new TagBuilder("div");
            div.Attributes["style"] = "position: fixed; right: 25px; bottom: 25px;";
            div.Attributes["class"] = "text-white";
            div.InnerHtml.AppendHtml(link);

            return div;
        }
    }
}
