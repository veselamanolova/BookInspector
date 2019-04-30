
namespace BookInspector.Web.Mappers.Contracts
{
    public interface IBookViewModelMapper<TEntity, TViewModel>
    {
        TViewModel MapFrom(TEntity entity);
    }
}
