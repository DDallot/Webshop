namespace Webshop.API.Contracts.v2.Common;

public class NoResult
{
    public bool HasError { get; set; }
    public List<string> Errors { get; set; }
}