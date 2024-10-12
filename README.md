# Dbarone.Net.Fake
A .NET Fake Dataset Generator.

This library contains a number of classes to faciliate the creation of fake data. This project will be used in some of my other data related projects where large amount of synthetic data is required for testing and demonstration purposes.

## Random Number Generators
At the core of this library are a number of random number generators. A number of different algorithms are supplied depending on the random number distribution required:

### Continuous Uniform Distribution
- Linear Congruential Generator (LCG): A pseudo-random number generator (https://en.wikipedia.org/wiki/Linear_congruential_generator) supplied by the `Lcg` class. The class can be configured using a number of pre-supplied parameters to mimick a number of common compilers .

### Normal Distribution
- Box Muller Transform: The Box-Muller transform is a random number generator that generates numbers with a normal distribution.



## Library Reference
For a full reference of this library, please refer to the [API documentation](https://html-preview.github.io/?url=https://github.com/davidbarone/Dbarone.Net.Fake/blob/main/Dbarone.Net.Fake.html).

David Barone Apr-2024