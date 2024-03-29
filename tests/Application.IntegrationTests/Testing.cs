﻿using Dietonator.Domain.Common;
using Dietonator.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using NUnit.Framework;
using Respawn;

namespace Dietonator.Application.IntegrationTests;

[SetUpFixture]
public partial class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static Checkpoint _checkpoint = null!;
    private static Guid? _currentUserId;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();

        _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { "__EFMigrationsHistory" },
            DbAdapter = DbAdapter.Postgres
        };
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static Guid? GetCurrentUserId()
    {
        return Guid.Parse("7308E922-31B5-4EEB-B5B8-4F07918E9722");
    }

    public static async Task<Guid> RunAsDefaultUserAsync()
    {
        return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
    }

    public static async Task<Guid> RunAsAdministratorAsync()
    {
        return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { "Administrator" });
    }

    public static async Task<Guid> RunAsUserAsync(string userName, string password, string[] roles)
    {
        //    using var scope = _scopeFactory.CreateScope();

        //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        //    var user = new ApplicationUser { UserName = userName, Email = userName };

        //    var result = await userManager.CreateAsync(user, password);

        //    if (roles.Any())
        //    {
        //        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //        foreach (var role in roles)
        //        {
        //            await roleManager.CreateAsync(new IdentityRole(role));
        //        }

        //        await userManager.AddToRolesAsync(user, roles);
        //    }

        //    if (result.Succeeded)
        //    {
        //        _currentUserId = user.Id;

        //        return _currentUserId;
        //    }

        //    var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

        //    throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");

        return await Task.FromResult(Guid.Parse("7308E922-31B5-4EEB-B5B8-4F07918E9722"));    
    }

    public static async Task ResetState()
    {
        using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            await conn.OpenAsync();

            await _checkpoint.Reset(conn);
        }

        _currentUserId = null;
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : BaseEntity
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}
