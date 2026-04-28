using Microsoft.EntityFrameworkCore;
using Shop.Api.Dtos;
using Shop.Contracts;
using Shop.Data.Contracts;

namespace Shop.Api.Extensions;

internal static class WebApplicationExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapProducts();
        return app;
    }

    private static WebApplication MapProducts(this WebApplication app)
    {
        var products = app.MapGroup("/products");

        products.MapGet("/", async (IUnitOfWork uow) =>
            await uow.GetRepository<Product>().Query.ToListAsync());

        products.MapGet("/{id:guid}", async (Guid id, IUnitOfWork uow) =>
        {
            var product = await uow.GetRepository<Product>().Query.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(product);
        });

        products.MapPost("/", async (ProductRequest request, IUnitOfWork uow) =>
        {
            var repo = uow.GetRepository<Product>();
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            };
            repo.Add(product);
            await uow.SaveAsync();
            return Results.Created($"/products/{product.Id}", product);
        });

        products.MapPut("/{id:guid}", async (Guid id, ProductRequest request, IUnitOfWork uow) =>
        {
            var product = await uow.GetRepository<Product>().Query.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return Results.NotFound();
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.UpdatedAtUtc = DateTime.UtcNow;
            await uow.SaveAsync();
            return Results.Ok(product);
        });

        products.MapDelete("/{id:guid}", async (Guid id, IUnitOfWork uow) =>
        {
            var repo = uow.GetRepository<Product>();
            var product = await repo.Query.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return Results.NotFound();
            }

            repo.Remove(product);
            await uow.SaveAsync();
            return Results.NoContent();
        });

        return app;
    }
}
