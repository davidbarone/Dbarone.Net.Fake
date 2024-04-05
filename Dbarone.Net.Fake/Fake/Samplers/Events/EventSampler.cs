using System;

namespace Dbarone.Net.Fake;
/// <summary>
/// Generates events.
/// </summary>
public class EventSampler<T, U> : ISampler<T> where T : IEvent
{
    public DateTime PreviousDate { get; set; }
    public DateTime StartDate { get; set; }
    public Func<DateTime, double> OnLambda { get; set; }
    public U Context { get; set; }
    public IRandom<double> Random { get; set; }
    public Func<U, DateTime, T> OnEvent { get; set; }

    public EventSampler(DateTime startDate, Func<DateTime, double> onLambda, U context, Func<U, DateTime, T> onEvent)
    {
        this.StartDate = startDate;
        this.PreviousDate = this.StartDate;
        this.OnLambda = onLambda;
        this.Random = new ExponentialRandom(OnLambda(this.PreviousDate));
        this.OnEvent = onEvent;
        this.Context = context;
    }

    public T Next()
    {
        // Get a new value for Lambda based on PreviousDate
        this.Random = new ExponentialRandom(OnLambda(this.PreviousDate));
        this.PreviousDate = PreviousDate.AddDays(this.Random.Next());
        
        var data = OnEvent(Context, this.PreviousDate);
        return data;
    }
}