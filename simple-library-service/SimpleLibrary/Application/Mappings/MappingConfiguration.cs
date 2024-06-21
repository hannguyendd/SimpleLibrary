using Mapster;
using SimpleLibrary.Application.Contracts.Dtos.Books;
using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Application.Mappings;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // config.NewConfig<Discount, DiscountDto>()
        //            .Map(dest => dest.Id, src => src.Id.ToString());

        // config.NewConfig<CreateDiscountDto, Discount>()
        //     .Map(dest => dest.Id, src => src.Id != null ? new ObjectId(src.Id) : ObjectId.GenerateNewId());

        config.NewConfig<Book, BookDto>();
        config.NewConfig<BorrowedBook, BorrowedBookDto>();
    }
}