namespace Squeezer.Services
{
    public interface IModelManager<T>
    {
        public T Create(T model);
        public T Get(int id);
        public IEnumerable<T> GetAll();
        public bool IsDuplicate(T model);
    }
}
