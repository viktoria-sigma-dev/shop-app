using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Constraints;
using UsersApp.Controllers;
using UsersApp.Model;
using UsersApp.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using UsersApp.Services;

namespace UsersApp.Repository
{
    public class UserRepository(FileService fileService)
    {
        private readonly FileService _fileService = fileService;

        public List<User> GetAll()
        {
            return this._fileService.LoadFromFile<List<User>>() ?? new List<User>();
        }

        public User? GetUser(int id) => this.GetAll().FirstOrDefault(user => user.Id == id);
        public User? GetUser(string email) => this.GetAll().FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public void SaveUsers(List<User> users)
        {
            this._fileService.SaveToFile<List<User>>(users);
        }

        public User Create(CreateUserDTO dto)
        {
            var users = this.GetAll();
            if (users.Any(user => user.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"User with email '{dto.Email}' already exists.");
            }
            var newUser = new User
            {
                Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Comments = Array.Empty<Comment>()
            };
            users.Add(newUser);
            SaveUsers(users);
            return newUser;
        }

        public User Update(int id, UpdateUserDTO dto)
        {
            var users = this.GetAll();
            var user = users.FirstOrDefault(user => user.Id == id) ?? throw new KeyNotFoundException($"User with ID {id} not found.");
            if (!string.IsNullOrWhiteSpace(dto.FirstName))
            {
            user.FirstName = dto.FirstName;                
            }
            if (!string.IsNullOrWhiteSpace(dto.LastName))
            {
            user.LastName = dto.LastName;                
            }
            if (!string.IsNullOrWhiteSpace(dto.DateOfBirth))
            {
            user.DateOfBirth = dto.DateOfBirth;                
            }

            SaveUsers(users);

            return user;
        }

        public bool Delete(int id)
        {
            var users = this.GetAll();
            var user = users.FirstOrDefault(u => u.Id == id) ?? throw new KeyNotFoundException($"User with ID {id} not found.");
            users.Remove(user);
            SaveUsers(users);

            return true;
        }
    }
}