using Application.ViewModel.Value;
using Application.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Service
{
    public interface IValueService
    {
        Task<bool> AddAsync(ValueForCreateDto valueForCreateDto, CancellationToken cancellationToken = default(CancellationToken));

        bool Update(ValueForUpdateDto valueForUpdateDto);

        Task<bool> DeleteAsync(int id);

        Task<ValueForListDto> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<ValueForListDto>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> IsDuplicateAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
    }
}
