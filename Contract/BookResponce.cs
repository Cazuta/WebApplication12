namespace BookStore.Api.Contract
{
    public record BookResponce
        ( Guid id,
        string Title,
        string description,
        decimal Price);
    
    
}
