using System.Data;
using Bogus;
using Dapper;
using VeteransMuseum.Application.Abstractions.Data;
using VeteransMuseum.Domain.Shared;

namespace VeteransMuseum.WebApi.Extensions;

internal static class SeedDataExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using IDbConnection connection = sqlConnectionFactory.CreateConnection();

            var faker = new Faker();
            
            List<object> news = new();
            for (int i = 0; i < 100; i++)
            {
                news.Add(new
                {
                    Id = Guid.NewGuid(),
                    Title = faker.Company.CompanyName(),
                    Content = faker.Random.String(500),
                    ImageUrl = faker.Image.PicsumUrl(),
                    CreatedAt = DateTime.Now,
                    CrearedBy = faker.Person.FullName
                });
            }

            const string sql = """
               INSERT INTO public.news
               (id, title, "content", image_url, created_at, created_by)
               VALUES(@Id, @Title, @Content, @ImageUrl, @CreatedAt, @CreatedBy);
           """;
            
        }
    }