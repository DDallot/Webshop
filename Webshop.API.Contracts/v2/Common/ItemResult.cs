namespace Webshop.API.Contracts.v2.Common;
public class ItemResult<T> : NoResult
{
    public T Item { get; set; }
}
