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

    }
}
