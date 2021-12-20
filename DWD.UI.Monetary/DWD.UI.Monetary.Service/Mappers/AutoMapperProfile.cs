namespace DWD.UI.Monetary.Service.Mappers;

using AutoMapper;
using DWD.UI.Calendar;
using DWD.UI.Monetary.Service.Models;

/// <summary>
/// Provides a configuration profile for AutoMapper.
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
    /// </summary>
    public AutoMapperProfile() => this.CreateMap<Quarter, CalendarQuarterDto>().ReverseMap();
}
