using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Application.UseCases.OrderCases;
using UsersApp.src.Application.UseCases.ProductCases;
using UsersApp.src.Application.UseCases.TransactionCases;
using UsersApp.src.Application.UseCases.UserCases;

namespace UsersApp.src.WebApi.DI
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // User UseCases
            services.AddScoped<CreateUserUseCase>();
            services.AddScoped<DeleteUserUseCase>();
            services.AddScoped<GetAllUserUseCase>();
            services.AddScoped<GetOneUserUseCase>();
            services.AddScoped<GetBalanceUserUseCase>();
            services.AddScoped<GetBalanceHistoryUserUseCase>();
            services.AddScoped<GetOrdersUserUseCase>();
            services.AddScoped<UpdateUserUseCase>();

            // Product UseCases
            services.AddScoped<CreateProductUseCase>();
            services.AddScoped<DeleteProductUseCase>();
            services.AddScoped<GetAllProductUseCase>();
            services.AddScoped<GetOneProductUseCase>();

            // Transaction UseCases
            services.AddScoped<GetAllByUserTransactionUseCase>();
            services.AddScoped<GetAllByOrderTransactionUseCase>();

            // Order UseCases
            services.AddScoped<CreateOrderUseCase>();
            services.AddScoped<DeclineOrderUseCase>();
            services.AddScoped<GetAllOrderUseCase>();
            services.AddScoped<GetOneOrderUseCase>();
            services.AddScoped<UpdateStatusOrderUseCase>();

            return services;
        }
    }
}