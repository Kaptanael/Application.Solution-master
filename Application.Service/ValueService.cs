using Application.Service;
using Application.ViewModel.Value;
using Application.Data.UnitOfWork;
using Application.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Service
{
    public class ValueService : IValueService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ValueService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(ValueForCreateDto valueForCreateDto, CancellationToken cancellationToken = default(CancellationToken))
        {
            var valueToCreated = _mapper.Map<Value>(valueForCreateDto);
            await _uow.Values.AddAsync(valueToCreated);
            return await _uow.SaveAsync(cancellationToken) > 0 ? true : false;
        }

        public bool Update(ValueForUpdateDto valueForUpdateDto)
        {
            var valueForUpdated = _mapper.Map<Value>(valueForUpdateDto);
            var updatedValue = _uow.Values.Update(valueForUpdated);
            return _uow.Save() > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var valueForDeleted = await _uow.Values.GetByIdAsync(id);            
            var deletedValue = _uow.Values.Delete(valueForDeleted);
            return _uow.Save() > 0 ? true : false;
        }

        public async Task<IEnumerable<ValueForListDto>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {            
            var values = await _uow.Values.GetAllAsync();
            var valuesForReturned = _mapper.Map<IEnumerable<ValueForListDto>>(values);
            return valuesForReturned;
        }

        public async Task<ValueForListDto> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var value = await _uow.Values.GetByIdAsync(id);
            var valueForReturned = _mapper.Map<ValueForListDto>(value);
            return valueForReturned;
        }

        public async Task<bool> IsDuplicateAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var value = await _uow.Values.IsDuplicateAsync(name);
            return value;
        }        
    }
}
