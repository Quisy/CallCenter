using System.Threading.Tasks;
using CallCenter.API.DomainModel.DomainModels;
using CallCenter.API.Models.Membership;
using CallCenter.API.Repository.Interfaces.Membership;
using CallCenter.API.Services.Base;
using CallCenter.API.Services.Interfaces.Services.Membership;
using CallCenter.API.Utils;
using CallCenter.API.Utils.Helpers.Interfaces;

namespace CallCenter.API.Services.Services.Membership
{
    public class CustomerService : CrudService<CustomerModel, ICustomerRepository, DomainModel.DomainModels.Customer>, ICustomerService
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public CustomerService(ICustomerRepository repository, IModelMapper modelMapper, IAuthorizationRepository authorizationRepository) : base(repository, modelMapper)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<Result<CustomerModel>> GetCustomerByCredentials(string username, string password)
        {
            var user = await _authorizationRepository.FindUser(username, password);

            if (user == null)
                return Result<CustomerModel>.Error("User not found");

            var customerResult = Repository.GetCustomerByApplicationUserId(user.Id);

            if(customerResult.IsError)
                return Result<CustomerModel>.Error(customerResult.Messages);

            return Result<CustomerModel>.ErrorWhenNoData(ModelMapper.MapSingle<Customer, CustomerModel>(customerResult.Value));
            

        }
    }
}
