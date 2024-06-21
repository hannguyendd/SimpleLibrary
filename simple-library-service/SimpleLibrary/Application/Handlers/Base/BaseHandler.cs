using MapsterMapper;
using SimpleLibrary.Domain.Repositories;

namespace SimpleLibrary.Application.Handlers.Base;

public abstract class BaseHandler(IUnitOfWork uow, IMapper mapper)
{
    protected readonly IUnitOfWork _uow = uow;
    protected readonly IMapper _mapper = mapper;
}