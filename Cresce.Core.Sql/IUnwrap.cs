namespace Cresce.Core.Sql
{
    internal interface IUnwrap<out T>
    {
        T Unwrap();
    }

    internal interface IWrap<in T>
    {
        void Wrap(T entity);
    }
}