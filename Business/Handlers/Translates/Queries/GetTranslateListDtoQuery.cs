﻿using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
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
    public class GetTranslateListDtoQuery:IRequest<IDataResult<IEnumerable<TranslateDto>>>
    {
        public class GetTranslateListDtoQueryHandler : IRequestHandler<GetTranslateListDtoQuery, IDataResult<IEnumerable<TranslateDto>>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;
            public GetTranslateListDtoQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<TranslateDto>>> Handle(GetTranslateListDtoQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<TranslateDto>>(await _translateRepository.GetTranslateDtos());
            }
        }
    }
}
