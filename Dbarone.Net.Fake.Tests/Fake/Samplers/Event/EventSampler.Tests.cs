using System;
using System.Collections.Generic;
using System.Linq;
using Dbarone.Net.Fake;
using Xunit;

namespace Dbarone.Net.Fake.Tests;

public class EventSamplerTests
{

    public class TestEvent : IEvent
    {
        public DateTime EventDateTime { get; set; }
        public string Value { get; set; } = "";
    }

    [Fact]
    public void TestEventSampler()
    {
        List<WeightedItem<string>> items = new List<WeightedItem<string>>();
        items.Add(new WeightedItem<string>("Foo", 100));
        items.Add(new WeightedItem<string>("Bar", 700));
        items.Add(new WeightedItem<string>("Baz", 200));

        Func<DateTime, double> onLambda = (d) => 10;  // 10 events per day for all time

        WeightedRandomSampler<string> wrs = new WeightedRandomSampler<string>(items);
        Func<WeightedRandomSampler<string>, DateTime, TestEvent> onEvent = (wrs, dateTime) => new TestEvent { EventDateTime = dateTime, Value = wrs.Next() };

        EventSampler<TestEvent, WeightedRandomSampler<string>> es = new EventSampler<TestEvent, WeightedRandomSampler<string>>(
            new DateTime(2020, 01, 01),
            onLambda,
            wrs,
            onEvent);

        List<TestEvent> events = new List<TestEvent>();
        for (; ; )
        {
            var ev = es.Next();
            events.Add(ev);
            if (ev.EventDateTime > new DateTime(2021, 01, 01)) break;
        }
        // We should have 1 year of data which should be approx 3650 events (10 per day)
        Assert.InRange(events.Count, 3650 * 0.9, 3650 * 1.1);

        // Bar should be approx 70%
        Assert.InRange(events.Count(e => e.Value == "Bar"), 3650 * 0.7 * 0.95, 3650 * 0.7 * 1.05);
    }
}