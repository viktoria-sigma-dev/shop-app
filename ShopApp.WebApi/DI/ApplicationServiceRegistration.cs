using ShopApp.Application.UseCases.OrderCases;
using ShopApp.Application.UseCases.ProductCases;
using ShopApp.Application.UseCases.TransactionCases;
using ShopApp.Application.UseCases.UserCases;

namespace ShopApp.WebApi.DI
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // User UseCases
            services.AddScoped<CreateUserUseCase>();
            services.AddScoped<DeleteUserUseCase>();
            services.AddScoped<GetAllUserUseCase>();
            services.AddScoped<GetBalanceUserUseCase>();
            services.AddScoped<GetBalanceHistoryUserUseCase>();
            services.AddScoped<GetOneUserUseCase>();
            services.AddScoped<GetOrdersUserUseCase>();
            services.AddScoped<TopUpBalanceUserUseCase>();
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