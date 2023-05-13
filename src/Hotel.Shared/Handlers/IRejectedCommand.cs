namespace Hotel.Shared.Handlers;

public interface IRejectedCommand : ICommand
{
    public string Message { get; set; }
    public string Code { get; set; }
}
