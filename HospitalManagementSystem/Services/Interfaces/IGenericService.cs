namespace HospitalManagementSystem.Services.Interfaces
{
    public interface IGenericService<TViewModel, TEntity> where TViewModel : class where TEntity : class
    {
        Task<IEnumerable<TViewModel>> GetAllAsync();
        Task<TViewModel> GetByIdAsync(int id);
        Task<TViewModel> AddAsync(TViewModel viewModel);
        Task<TViewModel> UpdateAsync(TViewModel viewModel);
        Task<bool> DeleteAsync(int id);
    }
    

    
}
