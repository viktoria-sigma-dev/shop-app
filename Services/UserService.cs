using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.Dto;
using UsersApp.Mappers;
using UsersApp.Model;
using UsersApp.Repository;

namespace UsersApp.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            return this._userRepository.GetAll();
        }

        public List<UserDTO> GetAllDTOs()
        {
            var users = this._userRepository.GetAll();

            return UserMapper.ToDTOList(users);
        }


        public User? GetById(int id)
        {
            return this._userRepository.GetUser(id);
        }

        public UserDTO? GetDTOById(int id)
        {
            var user = this._userRepository.GetUser(id);

            if (user == null) return null;

            return UserMapper.ToDTO(user);
        }

        public UserDTO Create(CreateUserDTO dto)
        {
            return UserMapper.ToDTO(this._userRepository.Create(dto));
        }

        public UserDTO? Update(int id, UpdateUserDTO dto)
        {
            return UserMapper.ToDTO(this._userRepository.Update(id, dto));
        }

        public bool Delete(int id)
        {
            return this._userRepository.Delete(id);
        }
    }
}