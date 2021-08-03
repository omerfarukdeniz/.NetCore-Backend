using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Translates.Queries
{
    [SecuredOperation]
    public class GetTranslateQuery:IRequest<IDataResult<Translate>>
    {
        public int Id { get; set; }

        public class GetTranslateQueryHandler : IRequestHandler<GetTranslateQuery, IDataResult<Translate>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;
            public GetTranslateQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Translate>> Handle(GetTranslateQuery request, CancellationToken cancellationToken)
            {
                var translate = await _translateRepository.GetAsync(t => t.Id == request.Id);

                return new SuccessDataResult<Translate>(translate);
            }
        }
    }
}
