using System.Threading.Tasks;
using Test_01.Models.DTO;

namespace Test_01.Services
{
    public interface IOperatorService
    {
        Task<int> AddOrUpdateClientAndPhoneNumberAsync(ClientPhoneNumberDTO dto);
        Task<ClientPhoneNumberDetails> GetClientDetailsByPhoneNumberAsync(string phoneNumber);
        Task<bool> DeletePhoneNumberAndClientAsync(string phoneNumber);
    }
}