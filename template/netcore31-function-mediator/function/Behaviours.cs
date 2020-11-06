using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFunction
{
    public class LoggerPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        #region Fields

        private readonly ILogger _logger;
        
        #endregion

        #region Constructors

        public LoggerPipelineBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            
             _logger.LogInformation("REQUEST: {Name} {@Request}",
                typeof(TRequest).Name, request);

            return response;
        }

        #endregion
    }

    public class ValidationBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        #region Fields

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        #endregion

        #region Constructors

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        #endregion

        #region Public Methods

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Any()
                ? throw new ValidationException(failures)
                : next();
        }

        #endregion
    }
}
