namespace Squeezer.Services
{
    public interface IModelManager<T>
    {
        public T Create(T model);
        public List<T> Get(int? id);
        public bool IsDuplicate(T model);
    }
}
