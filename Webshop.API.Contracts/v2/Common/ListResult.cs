namespace Webshop.API.Contracts.v2.Common;

public class ListResult<T> : NoResult
{
    public List<T> Items { get; set; }
}