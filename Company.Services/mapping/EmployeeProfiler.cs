using AutoMapper;
using Company.Data.Entities;
using Company.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.mapping
{
    public class EmployeeProfiler : Profile
    {
        public EmployeeProfiler()
        {

            CreateMap<Employees, EmployeeDto>().ReverseMap();
        }
    }
}
