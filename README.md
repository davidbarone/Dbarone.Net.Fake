# Dbarone.Net.Fake
A .NET Fake Dataset Generator.

This library is a fake data generator, similar in fashion to a number of other fake generators. I've build my own one, so that I can control the output, and it's used in a number of my other data-related projects.

There are a few goals of this project:
1. To be able to fake realistic data. However, does not need to be too realistic.
2. Needs to be deterministic. Need to be able to regenerate the exact same datasets with a given seed value. This is important, as a number of tests will rely on the actual data values being used.

