namespace DevQuestions.Shared;

public class Result
{
    private Error? Error { get; }
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, Error? error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException();
            case false when error == Error.None:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public static Result Failure(Error? error) => new(false, error);
    public static Result Success() => new(true, null);
}

public class Result<TValue> : Result
{
    private readonly TValue _value = default!;

    private Result(bool isSuccess, Error? error, TValue value) : base(isSuccess, error)
    {
        _value = value;
    }

    private Result(bool isSuccess, Error? error) : base(isSuccess, error)
    {
    }

    public static Result<TValue> Success(TValue value) => new(true, null, value);
    
    public new static Result<TValue> Failure(Error error) => new(false, error);
    
    public TValue Value => IsSuccess ? _value : throw new InvalidOperationException();
}