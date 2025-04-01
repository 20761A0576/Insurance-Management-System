using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace InsuranceProviderManagementSystem.Models
{
    public class Documents
    {
        public IFormFile File { get; set; }
        public string ToEmailAddress {  get; set; } 

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
