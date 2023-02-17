namespace BookStore.Helpers
{
    using AutoMapper;
    using BookStore.DTO;
    using BookStore.Models;

    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        { 
          CreateMap<User, SecureUserDto>();
          CreateMap<SecureUserDto, User>();
          CreateMap<User, UserDto>();
          CreateMap<UserDto, User>();
          CreateMap<Booking, BookingDto>();
          CreateMap<BookingDto, Booking>();
          CreateMap<Product, ProductDto>();
          CreateMap<ProductDto, Product>();
          CreateMap<BookStoreItem, BookStoreItemDto>();
          CreateMap<BookStoreItemDto, BookStoreItem>();
        }
    }
}
