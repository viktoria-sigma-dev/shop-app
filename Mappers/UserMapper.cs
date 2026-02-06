using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.Dto;
using UsersApp.Model;

namespace UsersApp.Mappers
{
    public class UserMapper
    {
        public static UserDTO ToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };
        }
        public static List<UserDTO> ToDTOList(List<User> users)
        {
            return users
                .Select(ToDTO)
                .Where(dto => dto != null)
                .ToList()!;
        }
    }
}