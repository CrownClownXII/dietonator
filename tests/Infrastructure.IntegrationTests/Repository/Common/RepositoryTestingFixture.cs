using System;
using System.Threading;
using System.Threading.Tasks;
using Dietonator.Application.Common.Interfaces;
using Dietonator.Domain.Common;
using Dietonator.Domain.Entities;
using Dietonator.Infrastructure.Persistence;
using Dietonator.Infrastructure.Persistence.Interceptors;
using Dietonator.Infrastructure.Persistence.Respository;
using Dietonator.Infrastructure.Services;
using Dietonator.WebUI.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Npgsql;
using NUnit.Framework;
using Respawn;

namespace Infrastructure.IntegrationTests.Repository.Common;

[TestFixture]
public abstract class RepositoryTestingFixture
{
    private static readonly string PostgresConnectionString = "Host=localhost;Port=5431;Database=DietonatorDb_tests;Username=postgres;Password=admin;Timeout=200;IncludeErrorDetail=true;";
    private static Guid? _currentUserId; 
    private static Checkpoint? _checkpoint;

    private readonly Mock<IMediator> _mediatorMock;
    private readonly IDateTime _dateTime;
    private readonly Mock<ICurrentUserService> _currentUserService;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    protected readonly IApplicationDbContext _context;

    public RepositoryTestingFixture()
    {
        var options = DbOptions();

        _mediatorMock = new Mock<IMediator>(); 
        _dateTime = new DateTimeService();
        _currentUserService = new Mock<ICurrentUserService>();
        _auditableEntitySaveChangesInterceptor = new AuditableEntitySaveChangesInterceptor(_currentUserService.Object, _dateTime);

        var context = new ApplicationDbContext(options, _mediatorMock.Object, _auditableEntitySaveChangesInterceptor);

        context.Database.EnsureCreated();

        _context = context;

        MockCurrentUserServiceToReturn();
    }

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { "__EFMigrationsHistory" },
            DbAdapter = DbAdapter.Postgres
        };
    }

    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }

    private static DbContextOptions<ApplicationDbContext> DbOptions() 
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(PostgresConnectionString, ctx => ctx.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            .Options;
    }

    private void MockCurrentUserServiceToReturn() 
    {
        _currentUserService.Setup(c => c.UserId).Returns(_currentUserId);
    }

    private static async Task ResetState()
    {
        if (_checkpoint == null) 
        {
            throw new Exception("Checkpoint not initialized!!!");
        }

        using (var conn = new NpgsqlConnection(PostgresConnectionString))
        {
            await conn.OpenAsync();

            await _checkpoint.Reset(conn);
        }

        _currentUserId = null;
    }

     public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        return await _context.Set<TEntity>().FindAsync(keyValues);
    }

    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : BaseEntity
    {
        _context.Set<TEntity>().Add(entity);

        await _context.SaveChangesAsync(CancellationToken.None);
    }

    public async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        return await _context.Set<TEntity>().CountAsync();
    }
}