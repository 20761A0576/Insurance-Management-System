using InsuranceProviderManagementSystem.Models;

namespace InsuranceProviderManagementSystem.Repository
{
    public interface IPolicyRepository
    {
        IEnumerable<Policy> GetAllPolicies();

        IEnumerable<string> GetAllPolicyId();
        IEnumerable<string> GetAllSubCategoryId();

        Policy GetPolicyById(string id);

        int InsertPolicy(Policy policy);
        int UpdatePolicy(Policy policy);

        void DeletePolicy(string PolicyId);

        int savePolicy();

        IEnumerable<Policy> GetAllPoliciesUsingJoins();

        IEnumerable<Policy> GetAllPoiciesBySubCategoryId(string SubCatagoryId);

        IEnumerable<Policy> GetFilterByName();

        IEnumerable<Policy> GetPolicyByNames(string subCategoryId,string PolicyName);

    }
}
