#pragma checksum "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a8f52fb99f8362008bf9704472d7681168c30f98"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_ShowSkillsTitleComponent_ShowSkillsTitleComponent), @"mvc.1.0.view", @"/Views/Shared/Components/ShowSkillsTitleComponent/ShowSkillsTitleComponent.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/ShowSkillsTitleComponent/ShowSkillsTitleComponent.cshtml", typeof(AspNetCore.Views_Shared_Components_ShowSkillsTitleComponent_ShowSkillsTitleComponent))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a8f52fb99f8362008bf9704472d7681168c30f98", @"/Views/Shared/Components/ShowSkillsTitleComponent/ShowSkillsTitleComponent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23ac09be4bcfaa7f9829a01d1a134874eaae1f3b", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_ShowSkillsTitleComponent_ShowSkillsTitleComponent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<MyCms.ViewModels.Skills.ShowSkillsViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(65, 89, true);
            WriteLiteral("\r\n\r\n\r\n\r\n    <div class=\"col-md-6 col-xs-11  wow fadeInRight\" data-wow-delay=\"0.9s\">\r\n\r\n\r\n");
            EndContext();
#line 9 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(203, 18, true);
            WriteLiteral("            <span>");
            EndContext();
            BeginContext(222, 16, false);
#line 11 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
             Write(item.SkillsTitle);

#line default
#line hidden
            EndContext();
            BeginContext(238, 8, true);
            WriteLiteral(" <small>");
            EndContext();
            BeginContext(247, 24, false);
#line 11 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
                                      Write(item.Progress.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(271, 18, true);
            WriteLiteral("%</small></span>\r\n");
            EndContext();
#line 12 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
             if (item.Progress <= 30)
            {

#line default
#line hidden
            BeginContext(343, 181, true);
            WriteLiteral("                <div class=\"progress\">\r\n                    <div class=\"progress-bar progress-bar-danger\" role=\"progressbar\" aria-valuenow=\"20\" aria-valuemin=\"0\" aria-valuemax=\"100\"");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 524, "\"", 566, 3);
            WriteAttributeValue("", 532, "width:", 532, 6, true);
#line 15 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
WriteAttributeValue(" ", 538, item.Progress.ToString(), 539, 25, false);

#line default
#line hidden
            WriteAttributeValue("", 564, "%;", 564, 2, true);
            EndWriteAttribute();
            BeginContext(567, 33, true);
            WriteLiteral("></div>\r\n                </div>\r\n");
            EndContext();
#line 17 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"

            }
            else if (item.Progress > 30 && item.Progress <= 70)
            {

#line default
#line hidden
            BeginContext(697, 181, true);
            WriteLiteral("                <div class=\"progress\">\r\n                    <div class=\"progress-bar progress-bar-danger\" role=\"progressbar\" aria-valuenow=\"20\" aria-valuemin=\"0\" aria-valuemax=\"100\"");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 878, "\"", 920, 3);
            WriteAttributeValue("", 886, "width:", 886, 6, true);
#line 22 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
WriteAttributeValue(" ", 892, item.Progress.ToString(), 893, 25, false);

#line default
#line hidden
            WriteAttributeValue("", 918, "%;", 918, 2, true);
            EndWriteAttribute();
            BeginContext(921, 33, true);
            WriteLiteral("></div>\r\n                </div>\r\n");
            EndContext();
#line 24 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
            }
            else if (item.Progress > 70)
            {

#line default
#line hidden
            BeginContext(1026, 182, true);
            WriteLiteral("                <div class=\"progress\">\r\n                    <div class=\"progress-bar progress-bar-success\" role=\"progressbar\" aria-valuenow=\"20\" aria-valuemin=\"0\" aria-valuemax=\"100\"");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 1208, "\"", 1250, 3);
            WriteAttributeValue("", 1216, "width:", 1216, 6, true);
#line 28 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
WriteAttributeValue(" ", 1222, item.Progress.ToString(), 1223, 25, false);

#line default
#line hidden
            WriteAttributeValue("", 1248, "%;", 1248, 2, true);
            EndWriteAttribute();
            BeginContext(1251, 33, true);
            WriteLiteral("></div>\r\n                </div>\r\n");
            EndContext();
#line 30 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
            }

#line default
#line hidden
#line 30 "D:\ProjectMyCms\MyCms\MyCms.Web\Views\Shared\Components\ShowSkillsTitleComponent\ShowSkillsTitleComponent.cshtml"
             




        }

#line default
#line hidden
            BeginContext(1318, 16, true);
            WriteLiteral("    </div>\r\n\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<MyCms.ViewModels.Skills.ShowSkillsViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
