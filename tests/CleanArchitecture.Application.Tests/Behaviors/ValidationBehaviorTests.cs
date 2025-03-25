using CleanArchitecture.Application.Common.Behaviors;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using Xunit;

namespace CleanArchitecture.Application.Tests.Behaviors
{
    public class ValidationBehaviorTests
    {
        private class TestRequest : IRequest<string>
        {
            public string? Property { get; set; }
        }

        private class TestRequestValidator : AbstractValidator<TestRequest>
        {
            public TestRequestValidator()
            {
                RuleFor(x => x.Property).NotNull().NotEmpty();
            }

            // Pour faciliter les tests avec un validateur préconfiguré
            public static TestRequestValidator FailingValidator()
            {
                var validator = new TestRequestValidator();
                validator.RuleFor(x => x.Property).Must(_ => false).WithMessage("Validation échouée");
                return validator;
            }

            public static TestRequestValidator PassingValidator()
            {
                return new TestRequestValidator();
            }
        }

        [Fact]
        public async Task ValidationReussie_DoitContinuerExecution()
        {
            // Arrangement
            var validator = TestRequestValidator.PassingValidator();
            var validators = new List<IValidator<TestRequest>> { validator };
            
            var request = new TestRequest { Property = "Valeur valide" };
            var nextCalled = false;
            
            Task<string> Next() 
            {
                nextCalled = true;
                return Task.FromResult("Résultat");
            }
            
            var behavior = new ValidationBehavior<TestRequest, string>(validators);
            
            // Action
            var result = await behavior.Handle(request, Next, CancellationToken.None);
            
            // Assertion
            nextCalled.Should().BeTrue();
            result.Should().Be("Résultat");
        }

        [Fact]
        public async Task ValidationEchouee_DoitLeverException()
        {
            // Arrangement
            var validator = TestRequestValidator.FailingValidator();
            var validators = new List<IValidator<TestRequest>> { validator };
            
            var request = new TestRequest { Property = null };
            var behavior = new ValidationBehavior<TestRequest, string>(validators);
            
            // Action & Assertion
            await Assert.ThrowsAsync<ValidationException>(() => 
                behavior.Handle(request, () => Task.FromResult("Ne devrait pas être appelé"), CancellationToken.None));
        }

        [Fact]
        public async Task SansValidateurs_DoitContinuerExecution()
        {
            // Arrangement
            var validators = new List<IValidator<TestRequest>>();
            var request = new TestRequest { Property = null };
            var nextCalled = false;
            
            Task<string> Next() 
            {
                nextCalled = true;
                return Task.FromResult("Résultat");
            }
            
            var behavior = new ValidationBehavior<TestRequest, string>(validators);
            
            // Action
            var result = await behavior.Handle(request, Next, CancellationToken.None);
            
            // Assertion
            nextCalled.Should().BeTrue();
            result.Should().Be("Résultat");
        }
    }
}
