using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.Dto;
using UsersApp.Model;
using UsersApp.Repository;

namespace UsersApp.Services
{
    public class CommentService
    {
        public readonly CommentRepository _commentRepository;
        public CommentService(CommentRepository commentRepository)
        {
            this._commentRepository = commentRepository;
        }

        public List<Comment> GetAll(int userId)
        {
            return this._commentRepository.GetAll(userId);
        }

        public Comment Create(int userId, CreateCommentDTO comment)
        {
            return this._commentRepository.Create(userId, comment);
        }

        public bool Delete(int userId, int commentId)
        {
            return this._commentRepository.Delete(userId, commentId);
        }
    }
}