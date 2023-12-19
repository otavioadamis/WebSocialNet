using WebApplication1.Exceptions;
using WebSocialNet.Domain.DTOs.UserDTOs;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.Service
{
    public class UserService : IUserService
     {
        private readonly IUserRepo _userRepository;
        private readonly IAuthorizationService _authorizationService;
       
        public UserService(IUserRepo userRepository, IAuthorizationService authorizationService)
        {
            _userRepository = userRepository;
            _authorizationService = authorizationService;
        }

        public User GetById(string id)
        {
            var user = _userRepository.GetById(id) ?? throw new UserFriendlyException("Not found");
            return user;
        }

        public UserResponseDTO GetUserById(string _id)
        {
            var user = _userRepository.GetById(_id);

            var userModel = new UserResponseDTO();
            userModel = userModel.CreateModel(user);

            return userModel;
        }

        // Create
        public LoginResponseModel Signup(UserSignupDTO thisUser)
        {
            var checkEmail = _userRepository.GetByEmail(thisUser.Email);
            if (checkEmail != null)
            {
                throw new UserFriendlyException("Sorry! This email has already been used!");
            }

            thisUser.Password = BCrypt.Net.BCrypt.HashPassword(thisUser.Password);

            var newUser = new User()
            {
                Email = thisUser.Email,
                Name = thisUser.Name,
                Password = thisUser.Password,
                ProfilePicture = thisUser.ProfilePicture,
                Bio = thisUser.Bio
            };

            _userRepository.Add(newUser);
            _userRepository.SaveChanges();

            string token = _authorizationService.CreateToken(newUser);

            var userModel = new UserResponseDTO();
            userModel = userModel.CreateModel(newUser);

            var res = new LoginResponseModel
            {
                Token = token,
                User = userModel
            };

            return res;
        }

        public LoginResponseModel Login(UserLoginDTO thisUser)
        {
            var user = _userRepository.GetByEmail(thisUser.Email) ?? throw new UserFriendlyException("User Not found");

            bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(thisUser.Password, user.Password);
            if (!isPasswordMatch) { throw new UserFriendlyException("Invalid credentials!"); }

            string token = _authorizationService.CreateToken(user);

            var userModel = new UserResponseDTO();
            userModel = userModel.CreateModel(user);

            var res = new LoginResponseModel
            {
                Token = token,
                User = userModel
            };
            return res;
        }

        // Read
        public List<User> GetAllUsers()
        {
            var users = _userRepository.Get() ?? throw new UserFriendlyException("Theres nobody here!");
            return users;
        }

        // Update
        public UserResponseDTO UpdateInfo(string _id, UserUpdateInfoDTO updatedUser)
        {
            var user = _userRepository.GetById(_id) ?? throw new UserFriendlyException("User not found!");

            updatedUser.UpdateFields(user);
            _userRepository.SaveChanges();

            var userModel = new UserResponseDTO();
            userModel = userModel.CreateModel(user);

            return userModel;
        }

        //TODO Send the token to user Email.
        public string ForgotPassword(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null) { throw new UserFriendlyException("User not found!"); }

            string token =
                _authorizationService.CreateToken(user);

            return token;
        }

        public UserResponseDTO ChangePw(string _id, UserResetPwDTO thisUser)
        {
            var user = _userRepository.GetById(_id);
            if (user == null) { throw new UserFriendlyException("Cant find user!"); };

            bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(thisUser.Password, user.Password);
            if (isPasswordMatch) { throw new UserFriendlyException("Password needs to be different than the current password"); }

            if (thisUser.Password != thisUser.ConfirmPassword) { throw new UserFriendlyException("Confirm Password and Passoword fiels must be equal"); }

            thisUser.Password = BCrypt.Net.BCrypt.HashPassword(thisUser.Password);

            _userRepository.SaveChanges();

            var updatedUser = _userRepository.GetById(_id);

            var userModel = new UserResponseDTO();
            userModel = userModel.CreateModel(updatedUser);

            return userModel;
        }

        // Delete
        public void DeleteUser(string _id)
        {
            _userRepository.Delete(_id);
            _userRepository.SaveChanges();
        }
    }
}
