using InsuranceProviderManagementSystem.Exceptions;
using InsuranceProviderManagementSystem.Models;
using InsuranceProviderManagementSystem.Repository;

namespace InsuranceProviderManagementSystem.Business_Object
{
    public class PolicyBO
    {
        private readonly IPolicyRepository _policyRepository;

        public PolicyBO(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }
        public PolicyBO()
        {
            _policyRepository = new PolicyRepository();
        }

        public IEnumerable<Policy> GetAllPolicies()
        {
            return _policyRepository.GetAllPolicies();
        }

        public Policy GetPolicyById(string id)
        {
            return _policyRepository.GetPolicyById(id);
        }

        public int AddPolicy(Policy policy)
        {
            if (!policy.PolicyId.StartsWith('P'))
            {
                throw new PolicyValidationException("Policy Id Must be Start with 'P' ");
            }
            var AllPid = _policyRepository.GetAllPolicyId();
            if (AllPid.Contains(policy.PolicyId))
            {
                throw new PolicyNotFoundException("Policy Id is Already Exists in Policy Table");
            }
            if (policy.SumAssured <= 0)
            {
                throw new PolicyValidationException("Sum Assured Must Be Greater than 0");
            }

            if(policy.Tenure <=0)
            {
                throw new PolicyValidationException("Tenure Must Be Greater than 0");
            }
            if (!policy.SubCategoryId.StartsWith("SC"))
            {
                throw new PolicyValidationException("Sub-Category Id Must be Start with 'SC' ");
            }
            var AllSCid = _policyRepository.GetAllSubCategoryId();
            if (!AllSCid.Contains(policy.SubCategoryId))
            {
                throw new PolicyNotFoundException("Sub-Category Id is Not Found in Sub-Category Table");
            }
            policy.PremiumAmount = (0.85m * policy.SumAssured) / policy.Tenure;
            policy.StartDate = DateOnly.FromDateTime(DateTime.Now);
            policy.EndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(policy.Tenure));
            return _policyRepository.InsertPolicy(policy);
        }

        public int UpdatePolicy(Policy policy)
        {
            DateTime testDateTime = policy.StartDate.ToDateTime(TimeOnly.Parse("10:00 PM"));
            DateTime todayDate = DateTime.Now.Date;
            TimeSpan difference = todayDate - testDateTime;
            int daysDifference = (int)difference.TotalDays;
            if (daysDifference < 0)
            {
                throw new PolicyValidationException("Start date must be Past");
            }

            if (policy.SumAssured <= 0)
            {
                throw new PolicyValidationException("Sum Assured Must Be Greater than 0");
            }
            
            if (policy.Tenure <= 0)
            {
                throw new PolicyValidationException("Tenure Must Be Greater than 0");
            }
            if (!policy.SubCategoryId.StartsWith("SC"))
            {
                throw new PolicyValidationException("Sub-Category Id Must be Start with 'SC' ");
            }
            var AllSCid = _policyRepository.GetAllSubCategoryId();
            if (!AllSCid.Contains(policy.SubCategoryId))
            {
                throw new PolicyNotFoundException("Sub-Category Id is Not Found in Sub-Category Table");
            }
            policy.PremiumAmount = (0.85m * policy.SumAssured) / policy.Tenure;
            policy.EndDate = DateOnly.FromDateTime(testDateTime.AddYears(policy.Tenure));
            return _policyRepository.UpdatePolicy(policy);
        }

        public void DeletePolicy(string PolicyId)
        {
            _policyRepository.DeletePolicy(PolicyId);
        }

        public IEnumerable<string> GetAllPolicyId()
        {
            return _policyRepository.GetAllPolicyId();
        }
        
        public IEnumerable<string> GetAllSubCategoryId()
        {
            return _policyRepository.GetAllSubCategoryId();
        }

        public IEnumerable<Policy> GetAllPoliciesUsingJoins()
        {
            return _policyRepository.GetAllPoliciesUsingJoins();
        }

        public IEnumerable<Policy> GetAllPoiciesBySubCategoryId(string SubCatagoryId)
        {
            return _policyRepository.GetAllPoiciesBySubCategoryId(SubCatagoryId);
        }

        public IEnumerable<Policy> GetFilterByName()
        {
            return _policyRepository.GetFilterByName();
        }

        public IEnumerable<Policy> GetPolicyByNames(string SubCatagoryName,string PolicyName)
        {
            return _policyRepository.GetPolicyByNames(SubCatagoryName, PolicyName);
        }
    }
}
