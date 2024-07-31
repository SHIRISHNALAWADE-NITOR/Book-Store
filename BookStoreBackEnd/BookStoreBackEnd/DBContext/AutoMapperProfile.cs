using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Book, BookDTO>().ReverseMap();
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
        CreateMap<Address, AddressDTO>().ReverseMap();
        CreateMap<CartItem, CartItemDTO>().ReverseMap();
        CreateMap<Review, ReviewDTO>().ReverseMap();
        CreateMap<Role, RoleDTO>().ReverseMap();
        //CreateMap<IGrouping<string, Book>, CategoryDTO>()
        //    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Key))
        //    .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count()));
    }
}
