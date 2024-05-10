
using Test_01.Repositories;

public interface IOrderRepository {
    Task<int> AddOrUpdateClientAsync(Client client);
    Task<Client> GetClientByMobileNumberAsync(string mobileNumber);
    Task<bool> DeleteClientByMobileNumberAsync(string mobileNumber);
}