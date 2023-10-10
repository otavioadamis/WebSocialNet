using WebSocialNet.Domain.DTOs.UserDTOs;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IServices
{
    public interface IUserService
    {
        public User GetById(string id);
        public UserResponseDTO GetUserById(string _id);
        public LoginResponseModel Signup(UserSignupDTO thisUser);
        public LoginResponseModel Login(UserLoginDTO thisUser);
        public List<User> GetAllUsers();
        public UserResponseDTO UpdateInfo(string _id, UserUpdateInfoDTO updatedUser);
        public string ForgotPassword(string email);
        public UserResponseDTO ChangePw(string _id, UserResetPwDTO thisUser);
        public void DeleteUser(string _id);
    }
}
