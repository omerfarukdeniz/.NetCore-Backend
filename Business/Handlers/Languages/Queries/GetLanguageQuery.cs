﻿using Business.BusinessAspects;
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

namespace Business.Handlers.Languages.Queries
{
    [SecuredOperation]
    public class GetLanguageQuery:IRequest<IDataResult<Language>>
    {
        public int Id { get; set; }

        public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, IDataResult<Language>>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;
            public GetLanguageQueryHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Language>> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
            {
                var language = await _languageRepository.GetAsync(l => l.Id == request.Id);
                return new SuccessDataResult<Language>(language);
            }
        }
    }
}
