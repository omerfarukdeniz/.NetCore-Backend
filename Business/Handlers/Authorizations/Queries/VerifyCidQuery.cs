using Business.Adapters.PersonService;
using Business.Constants;
using Core.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations.Queries
{
    public class VerifyCidQuery:IRequest<IDataResult<bool>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CitizenId { get; set; }
        public int BirthYear { get; set; }

        public class VerifyCidQueryHandler : IRequestHandler<VerifyCidQuery, IDataResult<bool>>
        {
            private readonly IPersonService _personService;
            public VerifyCidQueryHandler(IPersonService personService)
            {
                _personService = personService;
            }
            public async Task<IDataResult<bool>> Handle(VerifyCidQuery request, CancellationToken cancellationToken)
            {
                var result = await _personService.VerifyCid(request.CitizenId, request.FirstName, request.LastName, request.BirthYear);

                if (result != true) 
                {
                    return new ErrorDataResult<bool>(result, Messages.CouldNotBeVerifyCid);
                }
                return new SuccessDataResult<bool>(result, Messages.VerifyCid);
            }
        }
    }
}
