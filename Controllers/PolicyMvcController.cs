using Microsoft.AspNetCore.Mvc;
using InsuranceProviderManagementSystem.Business_Object;
using InsuranceProviderManagementSystem.Models;
using InsuranceProviderManagementSystem.Repository;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using InsuranceProviderManagementSystem.Exceptions;
using InsuranceProviderManagementSystem.Services;

namespace InsuranceProviderManagementSystem.Controllers
{
    public class PolicyMvcController : Controller
    {
        //PolicyBO _policyBO = new PolicyBO();
        private readonly PolicyBO _policyBO;

        private readonly string _allowedContentType = "application/pdf";
        private readonly EmailService _emailService;

        //public PolicyMvcController(EmailService emailService)
        //{
        //    _emailService = emailService;
        //}

        public PolicyMvcController(IPolicyRepository policyRepository, EmailService emailService)
        {
            _policyBO = new PolicyBO(policyRepository);
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            IEnumerable<Policy> policies = _policyBO.GetAllPolicies();
            ViewBag.css = "table";
            return View("Views/PolicyMvc/Index.cshtml", policies);
            //return View();
        }
        public IActionResult GetAllPoliciesUsingJoins()
        {
            IEnumerable<Policy> policies = _policyBO.GetAllPoliciesUsingJoins();
            ViewBag.css = "table";
            return View("Views/PolicyMvc/GetAllPoliciesUsingJoins.cshtml", policies);
            //return View();
        }

        public IActionResult GetBySubCategoryId()
        {
            ViewBag.css = "id";
            return View("GetAllPoiciesBySubCategoryId");
        }
        [HttpPost]
        public IActionResult GetAllPoiciesBySubCategoryId(string SubCatagoryId)
        {
            IEnumerable<Policy> policies = _policyBO.GetAllPoiciesBySubCategoryId(SubCatagoryId);
            if (policies.IsNullOrEmpty())
            {
                ViewBag.message = SubCatagoryId + " Sub Catagory Id Not Exits in Policy Table";
                ViewBag.css = "id";
                return View("GetAllPoiciesBySubCategoryId");
            }
            else
            {
                ViewBag.css = "table";
                return View("Views/PolicyMvc/GetAllPoiciesBySubCategoryId.cshtml", policies);
            }
        }
        public IActionResult GetById()
        {
            //List<string> columns = new List<string> { "PolicyName", "PolicyNumber", "PolicyDate", "PolicyType" };
            //ViewBag.PolicyColumns = new SelectList(_policyBO.GetAllPolicyId());
            ViewBag.css = "id";
            return View("GetPolicyById");
        }

        [HttpPost]
        public IActionResult GetPolicyById(string PolicyId)
        {
            Policy policy = _policyBO.GetPolicyById(PolicyId);
            if (policy == null)
            {
                ViewBag.css = "id";
                ViewBag.message = PolicyId + " Policy Id Not Exits in Policy Table";
                return View();
            }
            else
            {
                ViewBag.css = "box";
                return View("Views/PolicyMvc/GetPolicyById.cshtml", policy);
            }
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            Policy policy = _policyBO.GetPolicyById(id);
            if (policy == null)
            {
                ViewBag.message = id + " Policy Id Not Exits in Policy Table";
                return View("Result");
            }
            else
            {
                ViewBag.css = "box";
                return View("Views/PolicyMvc/Details.cshtml", policy);
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Policy policy = _policyBO.GetPolicyById(id);
            if (policy == null)
            {
                ViewBag.message = id + " Policy Id Not Exits in Policy Table";
                return View("Result");
            }
            else
            {
                IEnumerable<string> ids = _policyBO.GetAllSubCategoryId();
                ViewBag.scids = ids;
                ViewBag.css = "update";
                return View("Views/PolicyMvc/Edit.cshtml", policy);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            _policyBO.DeletePolicy(id);
            ViewBag.message = " Policy Id :"+ id +" Delete Successfully in Policy Table";
            return View("Result");
        }
        public ActionResult PolicyTable()
        {
            IEnumerable<string> ids= _policyBO.GetAllSubCategoryId();
            ViewBag.scids = ids;
            ViewBag.css = "update";
            return View("AddPolicy");
        }

        [HttpPost]
        public ActionResult AddPolicy(Policy policy)
        {
            int count = 0;
            try
            {
                count = _policyBO.AddPolicy(policy);
            }
            catch (PolicyValidationException ex) 
            {
                ViewBag.css = "update";
                ViewBag.message = ex.Message;
                return View(policy);
            }
            catch (PolicyNotFoundException ex)
            {
                ViewBag.css = "update";
                ViewBag.message = ex.Message;
                return View(policy);
            }
            if (count > 0)
            {
                ViewBag.message = "Insert Successfully";
            }
            else
            {
                ViewBag.message = "Not Insert Successfully";
            }
            return View("Result");
        }

        [HttpPost]
        public ActionResult UpdatePolicy(Policy policy)
        {
            int count = 0;
            try
            {
                count = _policyBO.UpdatePolicy(policy);
            }
            catch (PolicyValidationException ex)
            {
                ViewBag.css = "update";
                ViewBag.message = ex.Message;
                return View("Views/PolicyMvc/Edit.cshtml", policy);
            }
            catch (PolicyNotFoundException ex)
            {
                ViewBag.css = "update";
                ViewBag.message = ex.Message;
                return View("Views/PolicyMvc/Edit.cshtml", policy);
            }
            if (count > 0)
            {
                ViewBag.message = "Update Successfully";
            }
            else
            {
                ViewBag.message = "Not Upadate Successfully";
            }
            return View("Result");
        }

        public IActionResult GetPolicyByName()
        {
            ViewBag.css = "id";
            return View("GetPolicyByNames");
        }

        [HttpPost]
        public IActionResult GetPolicyByNames(string SubCategoryName, string PolicyName)
        {
            var policies = _policyBO.GetPolicyByNames(SubCategoryName, PolicyName);
            if (policies.IsNullOrEmpty())
            {
                ViewBag.message = "Sub Category Name and Policy Name does not exist in Policy Table";
                ViewBag.css = "id";
                return View("GetPolicyByNames");
            }
            ViewBag.css = "table";
            return View("Views/PolicyMvc/GetPolicyByNames.cshtml", policies);
        }

        public IActionResult GetFilterByName()
        {
            var policies = _policyBO.GetFilterByName();
            if (policies.IsNullOrEmpty())
            {
                ViewBag.message = "With this Filters does not exist in Policy Table";
                return View("Result");
            }
            ViewBag.css = "table";
            return View("Views/PolicyMvc/GetFilterByName.cshtml", policies);
        }


        [HttpGet]
        public IActionResult Upload()
        {
            ViewBag.css = "id";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(Documents model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                ViewData["Message"] = "No file selected.";
                ViewBag.css = "id";
                return View();
            }

            // Check if the file is a PDF
            if (model.File.ContentType != _allowedContentType ||
                Path.GetExtension(model.File.FileName).ToLower() != ".pdf")
            {
                ViewData["Message"] = "Only PDF files are allowed.";
                ViewBag.css = "id";
                return View();
            }

            // Save the file to a specified directory
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, model.File.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            //await _emailService.SendEmailWithAttachment(filePath, model.File.FileName);
            //string filePath = Path.Combine("uploads", model.File.FileName);
            await _emailService.SendEmailWithAttachment(filePath, model.File.FileName,model);
            ViewData["Message"] = "Email Send successfully!";
            ViewBag.css = "id";
            return View();
        }

    }
}
