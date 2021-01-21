namespace Cresce.Core.Sql
{
    internal interface IUnwrap<out T>
    {
        T Unwrap();
    }
}
