using AutoMapper;
using Company.Data.Entities;
using Company.repository.interfaces;
using Company.Services.Dto;
using Company.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Add(DepartmentDto depDto)
        {

            Department dep = _mapper.Map<Department>(depDto);
            _unitOfWork.DepartmentRepository.Add(dep);
        }

        public void Delete(DepartmentDto entity)
        {
            var dep = _unitOfWork.DepartmentRepository.GetById(entity.Id);

            dep.IsDeleted = true;
            _unitOfWork.DepartmentRepository.Delete(dep);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            IEnumerable<Department> dep = _unitOfWork.DepartmentRepository.GetAll().Where(x => x.IsDeleted == false);

            // Map the collection of Department to a collection of DepartmentDto
            IEnumerable<DepartmentDto> depDto = _mapper.Map<IEnumerable<DepartmentDto>>(dep);

            return depDto;
        }

        public DepartmentDto GetById(int? id)
        {
            if (id is null)
            {
                return null;
            }
            else
            {

                Department dep = _unitOfWork.DepartmentRepository.GetById((int)id);

                DepartmentDto depDto = _mapper.Map<DepartmentDto>(dep);
                return depDto;
            }

        }

        public void Update(DepartmentDto entity)
        {
            if (entity.Name is null)
            {
                throw new Exception("Name cant be null");
            }
            if (entity.code is null)
            {
                throw new Exception("Code cant be null");

            }
            var dep = _unitOfWork.DepartmentRepository.GetById(entity.Id);
            dep.code = entity.code;
            dep.Name = entity.Name;
            _unitOfWork.DepartmentRepository.Update(dep);

        }
    }
}
