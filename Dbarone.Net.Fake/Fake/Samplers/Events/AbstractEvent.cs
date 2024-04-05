using System;

/// <summary>
/// Represents an event with associated data payload.
/// </summary>
/// <typeparam name="T"></typeparam>
public class AbstractEvent<T>
{
    public DateTime EventDateTime { get; set; }
    public T Data { get; set; } = default!;
}