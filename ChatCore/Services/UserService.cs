using AutoMapper;
using ChatCore.DTO;
using ChatCore.Services.Interfaces;
using ChatData.Entities;
using ChatData.Repositories.Interfaces;

namespace ChatCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddUserAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var userId = await _userRepository.AddUsersAsync(user);
            return userId;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserDTO>>(await _userRepository.GetAllUsersAsync());
        }
    }
}
