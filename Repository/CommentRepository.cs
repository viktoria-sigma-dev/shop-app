using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Routing.Constraints;
using UsersApp.Controllers;
using UsersApp.Model;
using UsersApp.Dto;

namespace UsersApp.Repository
{
    public class CommentRepository(UserRepository userRepository)
    {
        private readonly UserRepository _userRepository = userRepository;

        public List<Comment> GetAll(int userId)
        {
            var user = this._userRepository.GetUser(userId) ?? throw new KeyNotFoundException($"User with ID {userId} not found.");
            return user.Comments?.ToList() ?? new List<Comment>();
        }

        public Comment Create(int userId, CreateCommentDTO comment)
        {
            var users = this._userRepository.GetAll();
            var user = users.FirstOrDefault(user => user.Id == userId) ?? throw new KeyNotFoundException($"User with ID {userId} not found.");
            var nextCommentId = (user.Comments != null && user.Comments.Any())
                ? user.Comments.Max(c => c.Id) + 1
                : 1;
            var newComment = new Comment
            {
                Id = nextCommentId,
                Text = comment.Text,
                CreatedAt = DateTime.UtcNow
            };
            var commentList = user.Comments?.ToList() ?? new List<Comment>();
            commentList.Add(newComment);
            user.Comments = commentList.ToArray();
            this._userRepository.SaveUsers(users);

            return newComment;
        }

        public bool Delete(int userId, int commentId)
        {
            var users = this._userRepository.GetAll();
            var user = users.FirstOrDefault(user => user.Id == userId) ?? throw new KeyNotFoundException($"User with ID {userId} not found.");
            var commentList = user.Comments?.ToList() ?? new List<Comment>();
            var comment = commentList.FirstOrDefault(comment => comment.Id == commentId) ?? throw new KeyNotFoundException($"Comment with ID {commentId} not found.");
            commentList.Remove(comment);
            user.Comments = [..commentList];

            this._userRepository.SaveUsers(users);

            return true;
        }
    }
}