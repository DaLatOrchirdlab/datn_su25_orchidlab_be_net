﻿using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.User.CreateUser
{
    public class CreateUserCommandhandler(IUserRepository userRepository,
        ICurrentUserService currentUserService) : IRequestHandler<CreateUserCommand, string>
    {
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Users user = new()
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                RoleID = request.RoleID,
                Status = true,
                Password = "12345678",
                Create_by = currentUserService.UserId,
                Create_date = DateTime.UtcNow,
            };

            userRepository.Add(user);
            return await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? $"Created User with ID: {user.ID}"
                : "Failed to create User.";
        }
    }
}
