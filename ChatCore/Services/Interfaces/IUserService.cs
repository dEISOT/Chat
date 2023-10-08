using ChatCore.DTO;

namespace ChatCore.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        public Task<Guid> AddUserAsync(UserDTO userDTO);
    }
}
