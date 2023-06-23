using AutoMapper;
using ChfApi.Dtos;
using ChfApi.Entities;

namespace ChfApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserDto, UserDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Test, TestDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.CategoryName));
            CreateMap<TestDto, Test>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Booking, BookingDto>()
                .ForMember(d => d.BookingDate, o => o.MapFrom(s => s.BookingDate.ToString("dd-MMM-yyyy")))
                .ForMember(d => d.DeliveryDate, o => o.MapFrom(s => s.DeliveryDate.ToString("dd-MMM-yyyy")))
                .ForMember(d => d.BookedBy, o => o.MapFrom(s => s.User.KnownAs))
                .ForMember(d => d.DoctorName, o => o.MapFrom(s => s.Doctor.DoctorName))
                .ForMember(d => d.Specialist, o => o.MapFrom(s => s.Doctor.Specialist));

            CreateMap<BookingDto, Booking>();
            CreateMap<BookingDetail, BookingDetailDto>()
                .ForMember(d => d.TestName, o => o.MapFrom(s => s.Test.Name));
            CreateMap<BookingDetailDto, BookingDetail>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();
            CreateMap<ExpenseCategory, ExpenseCategoryDto>();
            CreateMap<ExpenseCategoryDto, ExpenseCategory>();
            CreateMap<Expense, ExpenseDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.CategoryName))
                .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee.Name))
                .ForMember(d => d.ExpenseDate, o => o.MapFrom(s => s.ExpenseDate.ToString("dd-MMM-yyyy")));
            CreateMap<ExpenseDto, Expense>();
            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();
        }
    }
}
