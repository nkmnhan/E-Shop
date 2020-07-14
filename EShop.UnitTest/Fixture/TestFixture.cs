using AutoMapper;
using EShop.BackEnd.Data;
using EShop.BackEnd.Models;
using EShop.BackEnd.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace EShop.UnitTest.Fixture
{
    public class TestFixture : IDisposable
    {
        public TestFixture()
        {
            CreateDatabase();
        }

        public static List<Category> categories = new List<Category> {
            new Category{ Id = 1, Name = "CategoryBase1"},
            new Category{ Id = 2, Name = "CategoryBase2"},
            new Category{ Id = 3, Name = "CategoryBase3"},
            new Category{ Id = 4, Name = "CategoryBase4"}
        };

        public static List<Brand> brands = new List<Brand> {
            new Brand{ Id = 1, Name = "BrandBase1"},
            new Brand{ Id = 2, Name = "BrandBase2"},
            new Brand{ Id = 3, Name = "BrandBase3"},
            new Brand{ Id = 4, Name = "BrandBase4"}
        };

        public static List<Product> products = new List<Product> {
            new Product{
                Id = 1,
                Name = "ProducBase1",
                Brand = brands[0],
                ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        Category = categories[0]
                    }
                }
            },
            new Product{
                Id = 2,
                Name = "ProducBase2",
                Brand = brands[1],
                ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        Category = categories[1]
                    }
                }
            },
            new Product{
                Id = 3,
                Name = "ProducBase3",
                Brand = brands[2],
                ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        Category = categories[2]
                    }
                }
            },
            new Product{ Id = 4,
                Name = "ProducBase4",
                Brand = brands[3],
                ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        Category = categories[3]
                    }
                }
            }
        };

        private IServiceScope _serviceScope;
        private SqliteConnection _connection;

        public virtual IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

        public virtual IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceScope == null)
                {
                    _serviceScope = ConfigureServices(new ServiceCollection()).BuildServiceProvider().CreateScope();
                }

                return _serviceScope.ServiceProvider;
            }
        }

        public virtual ApplicationDbContext Context
            => ServiceProvider.GetRequiredService<ApplicationDbContext>();

        public List<Category> Categories => categories;

        public List<Brand> Brands => brands;

        public List<Product> Products => products;

        public List<User> Users = new List<User> {
            new User
            {
                UserName = "alice"
            },
            new User
            {
                UserName = "bob"
            }
        };

        public virtual IProductService ProductService
            => ServiceProvider.GetRequiredService<IProductService>();

        public virtual IOrderService OrderService
            => ServiceProvider.GetRequiredService<IOrderService>();

        public virtual ICategoryService CategoryService
            => ServiceProvider.GetRequiredService<ICategoryService>();

        public virtual IBrandService BrandService
            => ServiceProvider.GetRequiredService<IBrandService>();

        public virtual void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _serviceScope?.Dispose();
            _serviceScope = null;
        }

        public virtual IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(b => b.UseSqlite(_connection));

            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddLogging();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandServive>();
            return services;
        }

        private void CreateDatabase()
        {
            Dispose();
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            Context.Database.EnsureCreated();
            Context.AddRange(brands);
            Context.AddRange(categories);
            Context.AddRange(products);
            Context.SaveChanges();

            SeedUser();
        }

        private void SeedUser()
        {
            var userManager = ServiceProvider.GetRequiredService<UserManager<User>>();
            foreach (var user in Users)
            {
                userManager.CreateAsync(user, "Pass123$").Wait();
            }
        }
    }
}
