using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Kangaroo.Models;

namespace System.Web.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomHelpers
    {

        /// <summary>
        /// usage sample @Html.GenerateLink(MVC.Users.ActionNames.ConfigureUser)
        /// </summary>
        /// Parameters:
        ///   htmlHelper:
        ///     The HTML helper instance that this method extends.
        ///
        ///   actionName:
        ///     The name of the action.
        /// <returns></returns>
        public static string GenerateLink(this HtmlHelper htmlHelper, string actionName, object routeValues)
        {
            var urlHelper = new UrlHelper(GetRequestContext());
            return urlHelper.Action(actionName, routeValues);
        }

        /// <summary>
        /// Gets the request context.
        /// </summary>
        /// <returns></returns>
        private static RequestContext GetRequestContext()
        {
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            return new RequestContext(context, RouteTable.Routes.GetRouteData(context));
        }

        /// <summary>
        /// Menus the item.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, actionName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controllerName, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            li.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        /// <summary>
        /// Render all messages that have been set during execution of the controller action.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        /// 
        public static HtmlString RenderMessages(this HtmlHelper htmlHelper)
        {
            var messages = String.Empty;
            foreach (var messageType in Enum.GetNames(typeof(MessageType)))
            {
                var message = htmlHelper.ViewContext.ViewData.ContainsKey(messageType)
                                ? htmlHelper.ViewContext.ViewData[messageType]
                                : htmlHelper.ViewContext.TempData.ContainsKey(messageType)
                                    ? htmlHelper.ViewContext.TempData[messageType]
                                    : null;
                if (message != null)
                {
                    var messageScript = string.Format(
                    @"(function(){{
                          toastr.{0}(""{1}"");
                    }}())", messageType.ToLowerInvariant(), message.ToString());  
                    var messageBoxBuilder = new TagBuilder("script");
                    messageBoxBuilder.SetInnerText(messageScript.ToString());
                    
                    messages += HttpUtility.HtmlDecode(messageBoxBuilder.ToString());
                }
            }
            return MvcHtmlString.Create(messages);
        }

        public static string GetWebVersion(this HtmlHelper html)
        {
            var appInstance = html.ViewContext.HttpContext.ApplicationInstance;
            //given that you should then be able to do 
            var assemblyVersion = appInstance.GetType().BaseType.Assembly.GetName().Version;
            //note the use of the BaseType - see note below
            return assemblyVersion.ToString();
        }

        /// <summary>
        /// <para>Extension message to shows messages. </para>
        /// <para>The Message support HTML code.</para>
        /// <para>For example: this message it'll show in &lt;strong&gt;bold&lt;/strong&gt;</para>
        /// <para>Based on http://blogs.taiga.nl/martijn/2011/05/03/keep-your-users-informed-with-asp-net-mvc </para>
        /// 
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="showAfterRedirect">if set to <c>true</c> [show after redirect].</param>
        public static void ShowMessage(this Controller controller, MessageType messageType, string message, bool showAfterRedirect = false)
        {
            var messageTypeKey = messageType.ToString();
            if (showAfterRedirect)
            {
                controller.TempData[messageTypeKey] = message;
            }
            else
            {
                controller.ViewData[messageTypeKey] = message;
            }
        }
    }
}
