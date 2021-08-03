﻿using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Translates.Commands
{
    [SecuredOperation]
    public class DeleteTranslateCommand:IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteTranslateCommandHandler : IRequestHandler<DeleteTranslateCommand, IResult>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;
            public DeleteTranslateCommandHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteTranslateCommand request, CancellationToken cancellationToken)
            {
                var translateToDelete = _translateRepository.Get(x => x.Id == request.Id);

                _translateRepository.Delete(translateToDelete);
                await _translateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
