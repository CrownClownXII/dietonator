using Dietonator.Application.Common.Interfaces;
using Dietonator.Application.Common.Repository.Meals;
using Dietonator.Application.Meals.Commands.CreateMeal;
using Dietonator.Domain.Entities;
using Dietonator.Domain.Enums;
using FluentAssertions;
using FluentValidation;
using Moq;
using NUnit.Framework;
using FluentValidation.TestHelper;

namespace Dietonator.Application.UnitTests.Meals.Commands;

public class CreateMealTest
{
    private readonly Mock<IMealRepository> _mealRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly CreateMealCommandHandler _sut;

    private readonly Guid _currentUserId = new("50950a34-1eea-4cab-8b41-d921a0f8de4f");

    public CreateMealTest()
    {
        _mealRepositoryMock = new Mock<IMealRepository>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();

        _sut = new CreateMealCommandHandler(_mealRepositoryMock.Object, _currentUserServiceMock.Object);
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    [TestCase(" ")]
    public void ShouldThrowValidationExceptionWhenTitleInvalid(string title)
    {
        var command = CreateMealCommandFor(title);
        var validator = new CreateMealCommandValidator();
        
        FluentActions.Invoking(() =>
            validator.ValidateAndThrow(command)).Should().Throw<ValidationException>();    
    }

    [Test]
    public void ShouldPassValidationWhenTitleValid()
    {
        var command = CreateMealCommandFor("Test");
        var validator = new CreateMealCommandValidator();
        
        var validationResult = validator.Validate(command);    

        Assert.True(validationResult.IsValid);
    }

    [Test]
    public async Task ShouldCreateMeal()
    {
        CurrentUserServiceWithLoggedUser();

        var command = CreateMealCommandFor("Test");

        await _sut.Handle(command, CancellationToken.None);

        RepositoryAddWasCalledFor(command, Times.Once());
        RepositorySaveChangesWasCalledFor(Times.Once());
    }

    private void RepositoryAddWasCalledFor(CreateMealCommand command, Times times) 
    {
        _mealRepositoryMock.Verify(c => c.Add(It.Is<Meal>(x => 
            x.UserId == _currentUserId && 
            x.Name == command.Name &&
            x.ForDate == command.ForDate &&
            x.Type == command.Type)), times);
    }

    private void RepositorySaveChangesWasCalledFor(Times times) 
    {
        _mealRepositoryMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), times);
    }   

    private static CreateMealCommand CreateMealCommandFor(string name) 
    {
        return new CreateMealCommand()
        {
            Name = name,
            Type = MealTypeEnum.CalculableMeal,
            ForDate = DateOnly.FromDateTime(DateTime.Now)
        };
    }

    private void CurrentUserServiceWithLoggedUser() 
    {
        _currentUserServiceMock.Setup(c => c.UserId).Returns(_currentUserId);

    }
}
