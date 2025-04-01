using InsuranceProviderManagementSystem.Business_Object;
using InsuranceProviderManagementSystem.Models;
using InsuranceProviderManagementSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System;
using NuGet.Protocol;

namespace InsuranceProviderManagementSystem.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class PolicyApiController : ControllerBase
    {
        private readonly InsuranceProviderManagementSystemContext _context;

        public PolicyApiController(InsuranceProviderManagementSystemContext context)
        {
            _context = context;
        }

        [Route("[action]")]
        public string sayHello()
        {
            return "Hello Ram";
        }
        
        [Route("~/sayHello")]
        public string sayHelloOmit()
        {
            return "Hello Ram Omitting Controller level api";
        }

        //https://localhost:7203/PolicyApi/GetAllPolicies
        [Route("GetAllPolicies")]
        [HttpGet]
        public IActionResult GetAllPolicies()
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;
            var policies = _context.Policies.ToList();
            if (policies.IsNullOrEmpty())
            {
                return NotFound("Policy is Empty");
            }
            return Ok(policies);
        }
        
        [Route("GetPolicyId/{id}")]
        [HttpGet]
        // https://localhost:7203/PolicyApi/GetPolicyId/P1
        // https://localhost:7203/PolicyApi/GetPolicyId/P1/Car Insurance
        public ActionResult GetPolicyId(string id)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;
            var policy = _context.Policies.Find(id);
            if (policy==null)
            {
                return NotFound("Policy Not Found with this Policy Id : " + id);
            }
            return Ok(policy);
        }
        
        [Route("GetPolicyBySubCategoryId")]
        [HttpGet]
        // https://localhost:7203/PolicyApi/GetPolicyBySubCategoryId?id=SC2
        // https://localhost:7203/PolicyApi/GetPolicyBySubCategoryId?id=SC2&name=Car Insurance
        public IActionResult GetPolicyBySubCategoryId(string id)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;
            var policies = (from pol in _context.Policies
                            where pol.SubCategoryId == id
                            select pol);
            if (policies.IsNullOrEmpty())
            {
                return NotFound("Policy Not Found with this Sub-Category Id : " + id);
            }
            return Ok(policies);
        }

        //https://localhost:7203/PolicyApi/addPolicy
        [Route("[action]")]
        [HttpPost]
        public IActionResult addPolicy(Policy policy)
        {
            _context.Policies.Add(policy);
            int i = 0;
            try
            {
                i = _context.SaveChanges();
            }
            catch
            {
                return BadRequest("Not Insert Successfully");
            }
            return Ok("Insert Successfully");

        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult GetPolicyByIdFromBody([FromBody] string id)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;
            var policy = (from pol in _context.Policies
                            where pol.PolicyId == id
                            select pol);
            if (policy.IsNullOrEmpty())
            {
                return NotFound("Policy Not Found with this Policy Id : "+id);
            }
            return Ok(policy);
        }

        //https://localhost:7203/PolicyApi/updatePolicy
        [Route("[action]")]
        [HttpPut]
        public IActionResult updatePolicy(Policy policy)
        {
            _context.Entry(policy).State = EntityState.Modified;
            int i = 0;
            try
            {
                i = _context.SaveChanges();
            }
            catch
            {
                return BadRequest("Not Update Successfully");
            }
            return Ok("Update Successfully");
        }

        //https://localhost:7203/PolicyApi/deletePolicy/P12
        [Route("[action]/{id}")]
        [HttpDelete("{id}")]
        public IActionResult deletePolicy(string id)
        {
            var policy = _context.Policies.Find(id);
            if(policy == null)
            {
                return NotFound("Policy Id Not Found to Delete");
            }
            _context.Policies.Remove(policy);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return NotFound("Not Delete Successfully");
            }
            return Ok("Delete Successfully");
        }

        //https://localhost:7203/PolicyApi/GetFilterByName
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetFilterByName()
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;
            var policies = (from l in _context.Policies
                            where l.PolicyName.Contains("L") && l.PolicyName.Length > 12
                            select l).ToList();
            if (policies.IsNullOrEmpty())
            {
                return NotFound("Policy Not Found with this Filters");
            }
            return Ok(policies);
        }

        //https://localhost:7203/PolicyApi/GetPolicyByNames/Term Insurance/Life Insurance
        [Route("[action]/{SubCategoryName}/{PolicyName}")]
        [HttpGet]
        public IActionResult GetPolicyByNames(string SubCategoryName, string PolicyName)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;
            var policies = (from l in _context.Policies
                            where l.PolicyName == PolicyName && l.SubCategory.SubCategoryName == SubCategoryName
                            select l).ToList();
            if (policies.IsNullOrEmpty())
            {
                return NotFound("Policy Not Found with this Sub Category Name and Policy Name");
            }
            return Ok(policies);
        }

        //https://localhost:7203/PolicyApi/GetAllPoliciesUsingJoins
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetAllPoliciesUsingJoins()
        {
            _context.ChangeTracker.LazyLoadingEnabled = true;
            var policies = _context.Policies.Include("SubCategory").OrderBy(x => x.SubCategoryId).ToList();
            return Ok(policies);
        }

        //public void CleanupOutdatedRecords()
        //{
        //    var oneYearAgo = DateOnly.FromDateTime(DateTime.Now.AddYears(-1));
        //    var outdatedRecords = _context.Policies
        //        .Where(e => e.EndDate < oneYearAgo );

        //    _context.Policies.RemoveRange(outdatedRecords);
        //    _context.SaveChanges();
        //}
    }
}
