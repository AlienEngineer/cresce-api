namespace Cresce.Core
{
    public interface IUnwrap<out T>
    {
        T Unwrap();
    }

    public interface IWrap<in T>
    {
        void Wrap(T entity);
    }
}