using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.Data.Entities;
using Company.Services.Dto;

namespace Company.Services.Interfaces
{
    public interface IDepartmentService
    {
        DepartmentDto GetById(int? id);
        IEnumerable<DepartmentDto> GetAll();
        void Add(DepartmentDto entity);
        void Update(DepartmentDto entity);
        void Delete(DepartmentDto entity);
    }
}
