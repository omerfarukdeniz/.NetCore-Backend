﻿using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Logs.Queries
{
    [SecuredOperation]
    public class GetLogDtoQuery:IRequest<IDataResult<IEnumerable<LogDto>>>
    {
        public class GetLogDtoQueryHandler : IRequestHandler<GetLogDtoQuery, IDataResult<IEnumerable<LogDto>>>
        {
            private readonly ILogRepository _logRepository;
            private readonly IMediator _mediator;

            public GetLogDtoQueryHandler(ILogRepository logRepository, IMediator mediator)
            {
                _logRepository = logRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<LogDto>>> Handle(GetLogDtoQuery request, CancellationToken cancellationToken)
            {
                var result = await _logRepository.GetListAsync();
                List<LogDto> data = new List<LogDto>();
                foreach (var item in result)
                {
                    var jsonMessage = JsonConvert.DeserializeObject<LogDto>(item.MessageTemplate);
                    dynamic msg = JsonConvert.DeserializeObject(item.MessageTemplate);
                    dynamic valueList = msg.Parameters[0];
                    dynamic exceptionMessage = msg.ExceptionMessage;
                    valueList = valueList.Value.ToString();

                    var list = new LogDto
                    {
                        Id = item.Id,
                        Level = item.Level,
                        TimeStamp = item.TimeStamp,
                        Type = msg.Parameters[0].Type,
                        User = jsonMessage.User,
                        Value = valueList,
                        ExceptionMessage = exceptionMessage
                    };
                    data.Add(list);
                }
                return new SuccessDataResult<IEnumerable<LogDto>>(data);
            }
        }
    }
}
