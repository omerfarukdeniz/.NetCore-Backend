using Business.BusinessAspects;
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
    public class GetTranslatesByLangQuery:IRequest<IDataResult<Dictionary<string,string>>>
    {
        public string Lang { get; set; }

        public class GetTranslatesByLangQueryHandler : IRequestHandler<GetTranslatesByLangQuery, IDataResult<Dictionary<string, string>>>
        {
            private readonly ITranslateRepository _transtlateRepository;
            private readonly IMediator _mediator;
            public GetTranslatesByLangQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _transtlateRepository = translateRepository;
                _mediator = mediator;
            }

            public async Task<IDataResult<Dictionary<string, string>>> Handle(GetTranslatesByLangQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<Dictionary<string, string>>(await _transtlateRepository.GetTranslatesByLang(request.Lang));
            }
        }
    }
}
