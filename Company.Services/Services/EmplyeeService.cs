using AutoMapper;
using Azure;
using Company.Data.Entities;
using Company.repository.interfaces;
using Company.repository.repositories;
using Company.Services.Dto;
using Company.Services.Helper;
using Company.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Services
{
    public class EmplyeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmplyeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public void Add(EmployeeDto entity)
        {
            Console.WriteLine(entity);
            //Department dep = _mapper.Map<Department>(entity);

            var mappedEmplopyee = new EmployeeDto
            {
                Name = entity.Name,
                Age = entity.Age,
                Address = entity.Address,
                Salary = entity.Salary,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                HiringDate = entity.HiringDate,
                ImageUrl = entity.ImageUrl,
                DepartmentId = entity.DepartmentId,
             


            };
            mappedEmplopyee.ImageUrl = DocumentSettings.UploadFile(entity.Image, "Images");
            Employees emp = _mapper.Map<Employees>(mappedEmplopyee);

            _unitOfWork.EmployeeRepository.Add(emp);
            _unitOfWork.Complete();

        }

        public void Delete(EmployeeDto entity)
        {
            var emp = _unitOfWork.EmployeeRepository.GetById(entity.Id);

            emp.IsDeleted = true;
            _unitOfWork.EmployeeRepository.Delete(emp);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            IEnumerable<Employees> emps = _unitOfWork.EmployeeRepository.GetAll().Where(x => x.IsDeleted == false);
            IEnumerable<EmployeeDto> empsDto = _mapper.Map<IEnumerable<EmployeeDto>>(emps);

            return empsDto;

        }

        public EmployeeDto GetById(int? id)
        {
            if (id is null)
            {
                return null;
            }
            else
            {
                var emps = _unitOfWork.EmployeeRepository.GetById((int)id);
                EmployeeDto empsDto = _mapper.Map<EmployeeDto>(emps);

                return empsDto;
            }

        }

        public void Update(EmployeeDto entity)
        {
            if (entity.Name is null)
            {
                throw new Exception("Name cant be null");
            }

            var emp = _unitOfWork.EmployeeRepository.GetById(entity.Id);

            emp.Name = entity.Name;
            emp.Age = entity.Age;
            emp.Address = entity.Address;
            emp.Salary = entity.Salary;
            emp.Email = entity.Email;
            emp.PhoneNumber = entity.PhoneNumber;
            emp.ImageUrl = entity.ImageUrl;
            emp.HiringDate = entity.HiringDate;
            emp.DepartmentId = entity.DepartmentId;
            _unitOfWork.EmployeeRepository.Update(emp);
            _unitOfWork.Complete();


        }

        IEnumerable<EmployeeDto> IEmployeeService.GetEmployeeByName(string employeeName)
        {

            var emps = this._unitOfWork.EmployeeRepository.GetEmployeeByName(employeeName);
            IEnumerable<EmployeeDto> empsDto = _mapper.Map<IEnumerable<EmployeeDto>>(emps);

            return empsDto;
        }
    }


}
