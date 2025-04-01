using InsuranceProviderManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProviderManagementSystem.Repository
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly InsuranceProviderManagementSystemContext _context;

        public PolicyRepository(InsuranceProviderManagementSystemContext context)
        {
            _context = context;
        }

        public PolicyRepository()
        {
            _context = new InsuranceProviderManagementSystemContext();
        }
        public IEnumerable<Policy> GetAllPolicies()
        {
            return _context.Policies.ToList();
            //return _context.Policies.ToList();
        }

        public Policy GetPolicyById(string id)
        {
            return _context.Policies.Find(id);
        }

        public int InsertPolicy(Policy policy)
        {
            _context.Policies.Add(policy);
            return savePolicy();
        }

        public int savePolicy()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int UpdatePolicy(Policy policy)
        {
            _context.Entry(policy).State = EntityState.Modified;
            return savePolicy();
        }

        public void DeletePolicy(string PolicyId)
        {
            _context.Policies.Remove(GetPolicyById(PolicyId));
            savePolicy();
        }

        public IEnumerable<string> GetAllPolicyId()
        {
            return (from l in _context.Policies
                    select l.PolicyId).ToList();
        }

        public IEnumerable<Policy> GetAllPoliciesUsingJoins()
        {
            var polices = _context.Policies.Include("SubCategory").OrderBy(x => x.SubCategoryId).ToList();
            //var pol = (from p in _context.Policies
            //           join sc in _context.SubCategories
            //           on p.SubCategoryId equals sc.SubCategoryId
            //           where p.PolicyName == PolicyName && sc.SubCategoryId.Equals(p.SubCategoryId)
            //           select new SubCategory
            //           {
            //               SubCategoryName = sc.SubCategoryName,
            //               CategoryId = sc.CategoryId,
            //               Policies = new List<Policy>
            //               {
            //                   new Policy
            //                   {
            //                       PolicyName = p.PolicyName,
            //                       SumAssured = p.SumAssured,
            //                       Tenure = p.Tenure,
            //                       PremiumAmount = p.PremiumAmount,
            //                   }
            //               }
            //           }
            //           );
            return polices;
        }

        public IEnumerable<Policy> GetAllPoiciesBySubCategoryId(string SubCatagoryId)
        {
            return (from l in _context.Policies
                    where l.SubCategoryId == SubCatagoryId
                    select l).ToList();
        }

        public IEnumerable<Policy> GetFilterByName()
        {
            var policies = (from l in _context.Policies
                            where l.PolicyName.Contains("L") && l.PolicyName.Length > 12
                            select l).ToList();
            return policies;
        }

        public IEnumerable<Policy> GetPolicyByNames(string SubCategoryName, string PolicyName)
        {
            var policies = (from l in _context.Policies
                            where l.PolicyName == PolicyName && l.SubCategory.SubCategoryName == SubCategoryName
                            select l).ToList();
            return policies;
        }

        public IEnumerable<string> GetAllSubCategoryId()
        {
            return (from l in _context.SubCategories
                    select l.SubCategoryId).ToList();
        }
    }
}
