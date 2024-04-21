<a id='top'></a>
# Assembly: Dbarone.Net.Fake
## Contents
- [Dataset](#dataset)
  - [GetData](#datasetgetdata(datasetenum,dbaronenetcsvprocessrowdelegate))
  - [GetString](#datasetgetstring(datasetenum))
  - [GetResources](#datasetgetresources)
- [DatasetEnum](#datasetenum)
  - [en_GB_Names_Boy](#datasetenumen_gb_names_boy)
  - [en_GB_Names_Girl](#datasetenumen_gb_names_girl)
  - [Surnames_US_Census_2010](#datasetenumsurnames_us_census_2010)
  - [en_GB_Street_Names_Simple](#datasetenumen_gb_street_names_simple)
  - [en_GB_Street_Names_Compound](#datasetenumen_gb_street_names_compound)
  - [en_GB_Street_Types](#datasetenumen_gb_street_types)
  - [Stochastic_Model_Town](#datasetenumstochastic_model_town)
  - [Stochastic_Model_Region](#datasetenumstochastic_model_region)
  - [Stochastic_Model_Lorem_Ipsum](#datasetenumstochastic_model_lorem_ipsum)
  - [Scandanavian_Words](#datasetenumscandanavian_words)
  - [Product_Types](#datasetenumproduct_types)
- [Faker](#dbaronenetfakefaker)
- [AbstractRandom](#dbaronenetfakeabstractrandom`1)
- [BoxMullerTransform](#dbaronenetfakeboxmullertransform)
  - [#ctor](#dbaronenetfakeboxmullertransform#ctor)
  - [#ctor](#dbaronenetfakeboxmullertransform#ctor(dbaronenetfakeirandom{systemdouble},systemdouble,systemdouble))
  - [#ctor](#dbaronenetfakeboxmullertransform#ctor(dbaronenetfakeirandom{systemdouble},systemdouble,systemdouble,systemuint64))
  - [Mean](#dbaronenetfakeboxmullertransformmean)
  - [StdDev](#dbaronenetfakeboxmullertransformstddev)
  - [Random](#dbaronenetfakeboxmullertransformrandom)
  - [Next](#dbaronenetfakeboxmullertransformnext)
- [IRandom](#dbaronenetfakeirandom`1)
  - [Next](#dbaronenetfakeirandom`1next)
  - [Seed](#dbaronenetfakeirandom`1seed)
- [Lcg](#dbaronenetfakelcg)
  - [#ctor](#dbaronenetfakelcg#ctor(systemuint64))
  - [#ctor](#dbaronenetfakelcg#ctor)
  - [#ctor](#dbaronenetfakelcg#ctor(dbaronenetfakelcgparamsenum))
  - [#ctor](#dbaronenetfakelcg#ctor(dbaronenetfakelcgparamsenum,systemuint64))
  - [Parameters](#dbaronenetfakelcgparameters)
  - [Next](#dbaronenetfakelcgnext)
- [LcgParams](#dbaronenetfakelcgparams)
  - [M](#dbaronenetfakelcgparamsm)
  - [A](#dbaronenetfakelcgparamsa)
  - [C](#dbaronenetfakelcgparamsc)
  - [OutputMask](#dbaronenetfakelcgparamsoutputmask)
  - [Create](#dbaronenetfakelcgparamscreate(dbaronenetfakelcgparamsenum))
- [LcgParamsEnum](#dbaronenetfakelcgparamsenum)
  - [ZX81](#dbaronenetfakelcgparamsenumzx81)
  - [Knuth_Numerical_Recipes](#dbaronenetfakelcgparamsenumknuth_numerical_recipes)
  - [Borland_C](#dbaronenetfakelcgparamsenumborland_c)
  - [glibc](#dbaronenetfakelcgparamsenumglibc)
  - [ANSI_C](#dbaronenetfakelcgparamsenumansi_c)
  - [Borland_Delphi](#dbaronenetfakelcgparamsenumborland_delphi)
  - [Turbo_Pascal](#dbaronenetfakelcgparamsenumturbo_pascal)
  - [Microsoft_Visual_C](#dbaronenetfakelcgparamsenummicrosoft_visual_c)
  - [Microsoft_Visual_Basic](#dbaronenetfakelcgparamsenummicrosoft_visual_basic)
  - [POSIX](#dbaronenetfakelcgparamsenumposix)
- [AbstractSampler](#dbaronenetfakeabstractsampler`1)
- [InfoObject](#dbaronenetfakeinfoobject)
  - [ToDictionary](#dbaronenetfakeinfoobjecttodictionary)
- [PersonInfo](#dbaronenetfakepersoninfo)
  - [PersonId](#dbaronenetfakepersoninfopersonid)
  - [FirstName](#dbaronenetfakepersoninfofirstname)
  - [Surname](#dbaronenetfakepersoninfosurname)
  - [Sex](#dbaronenetfakepersoninfosex)
  - [DoB](#dbaronenetfakepersoninfodob)
- [ProductInfo](#dbaronenetfakeproductinfo)
  - [Sku](#dbaronenetfakeproductinfosku)
  - [ProductName](#dbaronenetfakeproductinfoproductname)
  - [Department](#dbaronenetfakeproductinfodepartment)
  - [Description](#dbaronenetfakeproductinfodescription)
  - [Popularity](#dbaronenetfakeproductinfopopularity)
  - [Price](#dbaronenetfakeproductinfoprice)
  - [Cost](#dbaronenetfakeproductinfocost)
  - [Material](#dbaronenetfakeproductinfomaterial)
  - [Color](#dbaronenetfakeproductinfocolor)
  - [#ctor](#dbaronenetfakeproductinfo#ctor(dbaronenetfakeproducttypeinfo))
- [ProductSampler](#dbaronenetfakeproductsampler)
- [ProductTypeInfo](#dbaronenetfakeproducttypeinfo)
  - [Department](#dbaronenetfakeproducttypeinfodepartment)
  - [Description](#dbaronenetfakeproducttypeinfodescription)
  - [Weight](#dbaronenetfakeproducttypeinfoweight)
  - [Popularity](#dbaronenetfakeproducttypeinfopopularity)
  - [Price](#dbaronenetfakeproducttypeinfoprice)
  - [Margin](#dbaronenetfakeproducttypeinfomargin)
  - [Variance](#dbaronenetfakeproducttypeinfovariance)
  - [Materials](#dbaronenetfakeproducttypeinfomaterials)
  - [Colors](#dbaronenetfakeproducttypeinfocolors)
- [EventSampler](#dbaronenetfakeeventsampler`2)
- [ISampler](#dbaronenetfakeisampler`1)
  - [Random](#dbaronenetfakeisampler`1random)
  - [Next](#dbaronenetfakeisampler`1next)
- [IncludeLineDelegate](#dbaronenetfakeincludelinedelegate)
- [MarkovChainTrainerConfiguration](#dbaronenetfakemarkovchaintrainerconfiguration)
  - [WordDelimiters](#dbaronenetfakemarkovchaintrainerconfigurationworddelimiters)
  - [PunctuationCharacters](#dbaronenetfakemarkovchaintrainerconfigurationpunctuationcharacters)
  - [Order](#dbaronenetfakemarkovchaintrainerconfigurationorder)
  - [Level](#dbaronenetfakemarkovchaintrainerconfigurationlevel)
  - [IncludeLine](#dbaronenetfakemarkovchaintrainerconfigurationincludeline)
  - [ProcessLine](#dbaronenetfakemarkovchaintrainerconfigurationprocessline)
- [MarkovChainModel](#dbaronenetfakemarkovchainmodel)
  - [Order](#dbaronenetfakemarkovchainmodelorder)
  - [Level](#dbaronenetfakemarkovchainmodellevel)
  - [Matrix](#dbaronenetfakemarkovchainmodelmatrix)
  - [Serialise](#dbaronenetfakemarkovchainmodelserialise)
  - [Deserialise](#dbaronenetfakemarkovchainmodeldeserialise(systemstring))
- [MarkovChainTrainer](#dbaronenetfakemarkovchaintrainer)
  - [Train](#dbaronenetfakemarkovchaintrainertrain(systemiostream,dbaronenetfakemarkovchaintrainerconfiguration))
- [ProcessLineDelegate](#dbaronenetfakeprocesslinedelegate)
- [StochasticMatrix](#dbaronenetfakestochasticmatrix)
  - [Rows](#dbaronenetfakestochasticmatrixrows)
- [StochasticMatrixElement](#dbaronenetfakestochasticmatrixelement)
  - [NextState](#dbaronenetfakestochasticmatrixelementnextstate)
  - [Occurences](#dbaronenetfakestochasticmatrixelementoccurences)
  - [Probability](#dbaronenetfakestochasticmatrixelementprobability)
- [StochasticMatrixRow](#dbaronenetfakestochasticmatrixrow)
- [SequenceSampler](#dbaronenetfakesequencesampler)
  - [Start](#dbaronenetfakesequencesamplerstart)
  - [Previous](#dbaronenetfakesequencesamplerprevious)
- [UniqueSampler](#dbaronenetfakeuniquesampler`1)
- [WeightedItem](#dbaronenetfakeweighteditem`1)
  - [Value](#dbaronenetfakeweighteditem`1value)
  - [Weight](#dbaronenetfakeweighteditem`1weight)
  - [#ctor](#dbaronenetfakeweighteditem`1#ctor(`0,systemdouble))
- [WeightedRandomSampler](#dbaronenetfakeweightedrandomsampler`1)
- [ExponentialRandom](#exponentialrandom)
  - [Lambda](#exponentialrandomlambda)
  - [Next](#exponentialrandomnext)
- [PoissonRandom](#poissonrandom)
  - [Lambda](#poissonrandomlambda)
  - [Next](#poissonrandomnext)
  - [AddressLine1](#addressinfoaddressline1)
  - [AddressLine2](#addressinfoaddressline2)
  - [Town](#addressinfotown)
  - [Region](#addressinforegion)
  - [Postcode](#addressinfopostcode)
  - [Country](#addressinfocountry)
  - [TownRegion](#addresssamplertownregion)
  - [RegionCountry](#addresssamplerregioncountry)
  - [RegionPostcode](#addresssamplerregionpostcode)
- [PersonSampler](#personsampler)
- [AbstractEvent](#abstractevent`1)
- [IEvent](#ievent)
- [MarkovChainLevel](#markovchainlevel)
  - [Word](#markovchainlevelword)
  - [Character](#markovchainlevelcharacter)
- [MarkovChainSampler](#markovchainsampler)
  - [CurrentState](#markovchainsamplercurrentstate)
  - [#ctor](#markovchainsampler#ctor(dbaronenetfakemarkovchainmodel))
  - [#ctor](#markovchainsampler#ctor(dbaronenetfakemarkovchainmodel,dbaronenetfakeirandom{systemdouble}))
- [IWeightedItem](#iweighteditem)



---
>## <a id='dataset'></a>type: Dataset
### Namespace:
``
### Summary
 Gets datasets from internal assembly resources. 

### Type Parameters:
None

>### <a id='datasetgetdata(datasetenum,dbaronenetcsvprocessrowdelegate)'></a>method: GetData
#### Signature
``` c#
Dataset.GetData(DatasetEnum dataset, Dbarone.Net.Csv.ProcessRowDelegate onProcessRow)
```
#### Summary
 Gets a dataset from an embedded dataset resource. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|dataset: |The dataset to get.|
|onProcessRow: |Callback function to map each row in the CSV file.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='datasetgetstring(datasetenum)'></a>method: GetString
#### Signature
``` c#
Dataset.GetString(DatasetEnum dataset)
```
#### Summary
 Gets the string value of a dataset resource. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|dataset: |The data to get.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='datasetgetresources'></a>method: GetResources
#### Signature
``` c#
Dataset.GetResources()
```
#### Summary
 Gets a list of all the resources available. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='datasetenum'></a>type: DatasetEnum
### Namespace:
``
### Summary
 List of datasets stored as resources in the assembly. 

### Type Parameters:
None

>### <a id='datasetenumen_gb_names_boy'></a>field: en_GB_Names_Boy
#### Summary
 Boys names. UK Office of National Statistics 2021 names. https://www.ons.gov.uk/peoplepopulationandcommunity/birthsdeathsandmarriages/livebirths/datasets/babynamesinenglandandwalesfrom1996 


<small>[Back to top](#top)</small>
>### <a id='datasetenumen_gb_names_girl'></a>field: en_GB_Names_Girl
#### Summary
 Girls names. UK Office of National Statistics 2021 names. https://www.ons.gov.uk/peoplepopulationandcommunity/birthsdeathsandmarriages/livebirths/datasets/babynamesinenglandandwalesfrom1996 


<small>[Back to top](#top)</small>
>### <a id='datasetenumsurnames_us_census_2010'></a>field: Surnames_US_Census_2010
#### Summary
 Surnames from the US 2010 census (top 1000 names). https://www.census.gov/topics/population/genealogy/data/2010_surnames.html 


<small>[Back to top](#top)</small>
>### <a id='datasetenumen_gb_street_names_simple'></a>field: en_GB_Street_Names_Simple
#### Summary
 Single-word street names. Based on http://data.gov.uk/dataset/os-locator (https://greem.co.uk/os/os_locator_streets.txt). Data as at 2010. names under 10 hits excluded. 


<small>[Back to top](#top)</small>
>### <a id='datasetenumen_gb_street_names_compound'></a>field: en_GB_Street_Names_Compound
#### Summary
 Dual-word street names. Based on http://data.gov.uk/dataset/os-locator (https://greem.co.uk/os/os_locator_streets.txt). Data as at 2010. names under 10 hits excluded. 


<small>[Back to top](#top)</small>
>### <a id='datasetenumen_gb_street_types'></a>field: en_GB_Street_Types
#### Summary
 Street types / suffixes. Based on http://data.gov.uk/dataset/os-locator (https://greem.co.uk/os/os_locator_streets.txt). Data as at 2010. names under 10 hits excluded. 


<small>[Back to top](#top)</small>
>### <a id='datasetenumstochastic_model_town'></a>field: Stochastic_Model_Town
#### Summary
 Stochastic model that can be used to generate words that sound like UK post towns. 


<small>[Back to top](#top)</small>
>### <a id='datasetenumstochastic_model_region'></a>field: Stochastic_Model_Region
#### Summary
 Stochastic model that can be used to generate words that sound like UK regions. 


<small>[Back to top](#top)</small>
>### <a id='datasetenumstochastic_model_lorem_ipsum'></a>field: Stochastic_Model_Lorem_Ipsum
#### Summary
 Stochastic model that can be used to generate words that sound like latin words (based on Lorem Ipsum corpus). 


<small>[Back to top](#top)</small>
>### <a id='datasetenumscandanavian_words'></a>field: Scandanavian_Words
#### Summary
 A collection of Scandanavian words for places, names, animals etc. Can be used to generate product names for homewares shop. 


<small>[Back to top](#top)</small>
>### <a id='datasetenumproduct_types'></a>field: Product_Types
#### Summary
 Metadata for product types, that can be used to generate actual products for a fake homewares store. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakefaker'></a>type: Faker
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Main class that generates fake data. 

### Type Parameters:
None


---
>## <a id='dbaronenetfakeabstractrandom`1'></a>type: AbstractRandom
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Base abstract class for all classes implementing generic IRandom. 

### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of random value to return.|


---
>## <a id='dbaronenetfakeboxmullertransform'></a>type: BoxMullerTransform
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Implementation of Box-Muller Transform. Generates pairs of independent, standard, normally distributed random numbers. see: https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform 

### Type Parameters:
None

>### <a id='dbaronenetfakeboxmullertransform#ctor'></a>method: #ctor
#### Signature
``` c#
BoxMullerTransform.#ctor()
```
#### Summary
 Creates a new BoxMullerTransform object. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeboxmullertransform#ctor(dbaronenetfakeirandom{systemdouble},systemdouble,systemdouble)'></a>method: #ctor
#### Signature
``` c#
BoxMullerTransform.#ctor(Dbarone.Net.Fake.IRandom<System.Double> random, System.Double mean, System.Double stdDev)
```
#### Summary
 Creates a new BoxMullerTransform object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|random: |A random number generator.|
|mean: |The mean value of the distribution.|
|stdDev: |The standard deviation of the distribution.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeboxmullertransform#ctor(dbaronenetfakeirandom{systemdouble},systemdouble,systemdouble,systemuint64)'></a>method: #ctor
#### Signature
``` c#
BoxMullerTransform.#ctor(Dbarone.Net.Fake.IRandom<System.Double> random, System.Double mean, System.Double stdDev, System.UInt64 seed)
```
#### Summary
 Creates a new BoxMullerTransform object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|random: |A random number generator.|
|mean: |The mean value of the distribution.|
|stdDev: |The standard deviation of the distribution.|
|seed: |An initial seed value for the random number generator.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeboxmullertransformmean'></a>property: Mean
#### Summary
 The mean of the distribution. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeboxmullertransformstddev'></a>property: StdDev
#### Summary
 The standard deviation of the distribution. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeboxmullertransformrandom'></a>property: Random
#### Summary
 The random number generator. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeboxmullertransformnext'></a>method: Next
#### Signature
``` c#
BoxMullerTransform.Next()
```
#### Summary
 Gets the next value. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakeirandom`1'></a>type: IRandom
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Represents a class that implements a random generator. 

### Type Parameters:
None

>### <a id='dbaronenetfakeirandom`1next'></a>method: Next
#### Signature
``` c#
IRandom.Next()
```
#### Summary
 Returns the next random value. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeirandom`1seed'></a>property: Seed
#### Summary
 The seed value. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakelcg'></a>type: Lcg
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Linear congruential generator. Pseudo-random number generator. Returns a double value between 0 (inclusive) and 1 (exclusive). 

### Type Parameters:
None

>### <a id='dbaronenetfakelcg#ctor(systemuint64)'></a>method: #ctor
#### Signature
``` c#
Lcg.#ctor(System.UInt64 seed)
```
#### Summary
 Creates a new Lcg instance with a provided seed. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|seed: |The initial seed.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcg#ctor'></a>method: #ctor
#### Signature
``` c#
Lcg.#ctor()
```
#### Summary
 Creates a new Lcg instance with a default seed. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcg#ctor(dbaronenetfakelcgparamsenum)'></a>method: #ctor
#### Signature
``` c#
Lcg.#ctor(Dbarone.Net.Fake.LcgParamsEnum parameters)
```
#### Summary
 Creates a new Lcg instance using specific parameters to define the algorithm type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|parameters: |The parameters to use.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcg#ctor(dbaronenetfakelcgparamsenum,systemuint64)'></a>method: #ctor
#### Signature
``` c#
Lcg.#ctor(Dbarone.Net.Fake.LcgParamsEnum parameters, System.UInt64 seed)
```
#### Summary
 Creates a new Lcg instance using specific parameters to define the algorithm type, and an initial seed. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|parameters: |The parameters to use.|
|seed: |The initial seed.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparameters'></a>property: Parameters
#### Summary
 The parameters for the Lcg instance. Defines the algorithm type used. defaults to the ANSI C implementation of the Lcg algorithm. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgnext'></a>method: Next
#### Signature
``` c#
Lcg.Next()
```
#### Summary
 Gets the next random value. See: https://en.wikipedia.org/wiki/Linear_congruential_generator 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakelcgparams'></a>type: LcgParams
### Namespace:
`Dbarone.Net.Fake`
### Summary
 List of initialisation parameters for linear congruent generator. 

### Type Parameters:
None

>### <a id='dbaronenetfakelcgparamsm'></a>property: M
#### Summary
 The modulus. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsa'></a>property: A
#### Summary
 The multiplier. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsc'></a>property: C
#### Summary
 The increment. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsoutputmask'></a>property: OutputMask
#### Summary
 The output mask used to return the final result. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamscreate(dbaronenetfakelcgparamsenum)'></a>method: Create
#### Signature
``` c#
LcgParams.Create(Dbarone.Net.Fake.LcgParamsEnum type)
```
#### Summary
 Creates a new LcgParams instance based on a type enum. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type enum to select the LcgParams to use.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakelcgparamsenum'></a>type: LcgParamsEnum
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Defines commonly used parameter-sets to initialise the linear congruential generator. Values can be found at: https://en.wikipedia.org/wiki/Linear_congruential_generator 

### Type Parameters:
None

>### <a id='dbaronenetfakelcgparamsenumzx81'></a>field: ZX81
#### Summary
 Modulus = 2^16 + 1, multiplier = 75, increment = 74 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsenumknuth_numerical_recipes'></a>field: Knuth_Numerical_Recipes
#### Summary
 Modulus = 2^32, multiplier = 1664525, increment = 1013904223 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsenumborland_c'></a>field: Borland_C
#### Summary
 Modulus = 2^31, multiplier = 22695477, increment = 1 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsenumglibc'></a>field: glibc
#### Summary
 Modulus = 2^31, multiplier = 1103515245, increment = 12345 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsenumansi_c'></a>field: ANSI_C
#### Summary
 Modulus = 2^31, multiplier = 1103515245, increment = 12345 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsenumborland_delphi'></a>field: Borland_Delphi
#### Summary
 Modulus = 2^32, multiplier = 134775813, increment = 1 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsenumturbo_pascal'></a>field: Turbo_Pascal
#### Summary
 Modulus = 2^32, multiplier = 134775813, increment = 1 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsenummicrosoft_visual_c'></a>field: Microsoft_Visual_C
#### Summary
 Modulus = 2^31, multiplier = 214013 , increment = 2531011 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsenummicrosoft_visual_basic'></a>field: Microsoft_Visual_Basic
#### Summary
 Modulus = 2^24, multiplier = 16598013 , increment = 12820163 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakelcgparamsenumposix'></a>field: POSIX
#### Summary
 Modulus = 2^48, multiplier = 25214903917 , increment = 11 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakeabstractsampler`1'></a>type: AbstractSampler
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Base abstract class for all classes implementing generic ISampler interface. 

### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of random value to return.|


---
>## <a id='dbaronenetfakeinfoobject'></a>type: InfoObject
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Base class for all info objects. 

### Type Parameters:
None

>### <a id='dbaronenetfakeinfoobjecttodictionary'></a>method: ToDictionary
#### Signature
``` c#
InfoObject.ToDictionary()
```
#### Summary
 Converts the current object to a dictionary. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakepersoninfo'></a>type: PersonInfo
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Represents a fake person. 

### Type Parameters:
None

>### <a id='dbaronenetfakepersoninfopersonid'></a>property: PersonId
#### Summary
 A unique person surrogate key. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakepersoninfofirstname'></a>property: FirstName
#### Summary
 The person's first / given name. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakepersoninfosurname'></a>property: Surname
#### Summary
 The person's last name. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakepersoninfosex'></a>property: Sex
#### Summary
 The person's sex. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakepersoninfodob'></a>property: DoB
#### Summary
 The person's date of birth. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakeproductinfo'></a>type: ProductInfo
### Namespace:
`Dbarone.Net.Fake`
### Summary
 A fake product. 

### Type Parameters:
None

>### <a id='dbaronenetfakeproductinfosku'></a>property: Sku
#### Summary
 Stock keeping unit. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproductinfoproductname'></a>property: ProductName
#### Summary
 The name of the product. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproductinfodepartment'></a>property: Department
#### Summary
 The department that the product is sold from. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproductinfodescription'></a>property: Description
#### Summary
 The description of the product. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproductinfopopularity'></a>property: Popularity
#### Summary
 The relative weight that the product type is sold. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproductinfoprice'></a>property: Price
#### Summary
 The mean price for the product type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproductinfocost'></a>property: Cost
#### Summary
 The standard cost of the product. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproductinfomaterial'></a>property: Material
#### Summary
 The main material used. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproductinfocolor'></a>property: Color
#### Summary
 The main color of the product. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproductinfo#ctor(dbaronenetfakeproducttypeinfo)'></a>method: #ctor
#### Signature
``` c#
ProductInfo.#ctor(Dbarone.Net.Fake.ProductTypeInfo productType)
```
#### Summary
 Creates a new product from a product type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|productType: |The product type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakeproductsampler'></a>type: ProductSampler
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Generates fake products from a fake Scandanavian homewares store. 

### Type Parameters:
None


---
>## <a id='dbaronenetfakeproducttypeinfo'></a>type: ProductTypeInfo
### Namespace:
`Dbarone.Net.Fake`
### Summary
 A fake product type - used to generate fake products. 

### Type Parameters:
None

>### <a id='dbaronenetfakeproducttypeinfodepartment'></a>property: Department
#### Summary
 The department that the product is sold from. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproducttypeinfodescription'></a>property: Description
#### Summary
 The description of the product. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproducttypeinfoweight'></a>property: Weight
#### Summary
 The relative weight with which to generate new products. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproducttypeinfopopularity'></a>property: Popularity
#### Summary
 The relative weight that the product type is sold. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproducttypeinfoprice'></a>property: Price
#### Summary
 The mean price for the product type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproducttypeinfomargin'></a>property: Margin
#### Summary
 The mean margin for the product type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproducttypeinfovariance'></a>property: Variance
#### Summary
 The standard deviation of the product type, expressed as a fraction of the price. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproducttypeinfomaterials'></a>property: Materials
#### Summary
 A comma-separated list of Material values for the product type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeproducttypeinfocolors'></a>property: Colors
#### Summary
 A comma-separated list of color values for the product type. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakeeventsampler`2'></a>type: EventSampler
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Generates events. 

### Type Parameters:
None


---
>## <a id='dbaronenetfakeisampler`1'></a>type: ISampler
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Interface for a samplter algorithm that uses a random number generator to implement a particular random sampler. 

### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of data returned by the sampler.|

>### <a id='dbaronenetfakeisampler`1random'></a>property: Random
#### Summary
 The random number generator to use in the sampler. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeisampler`1next'></a>method: Next
#### Signature
``` c#
ISampler.Next()
```
#### Summary
 The next value to return. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakeincludelinedelegate'></a>type: IncludeLineDelegate
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Delegate for callback function for determining if line in corpus should be included in the model. 

### Type Parameters:
None


---
>## <a id='dbaronenetfakemarkovchaintrainerconfiguration'></a>type: MarkovChainTrainerConfiguration
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Defines the configuration used to train a corpus. 

### Type Parameters:
None

>### <a id='dbaronenetfakemarkovchaintrainerconfigurationworddelimiters'></a>property: WordDelimiters
#### Summary
 String array of word delimiters. Typically space character. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakemarkovchaintrainerconfigurationpunctuationcharacters'></a>property: PunctuationCharacters
#### Summary
 Punctuation characters. When tokenising a string, any punctuation characters at the end of a word are separated from the word token. If the n-gram is a character, punctuation is removed. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakemarkovchaintrainerconfigurationorder'></a>property: Order
#### Summary
 Number of states to be considered when generating the next state. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakemarkovchaintrainerconfigurationlevel'></a>property: Level
#### Summary
 Defines the n-gram unit within the model. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakemarkovchaintrainerconfigurationincludeline'></a>property: IncludeLine
#### Summary
 Callback function which can be used to determine whether or not to include a particular line of a corpus. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakemarkovchaintrainerconfigurationprocessline'></a>property: ProcessLine
#### Summary
 Callback function which can be used to execute any pre-processing transformation on a line before it is processed. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakemarkovchainmodel'></a>type: MarkovChainModel
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Supplies the model for a Markov chain text generator. 

### Type Parameters:
None

>### <a id='dbaronenetfakemarkovchainmodelorder'></a>property: Order
#### Summary
 Number of states to be considered when generating the next state. 1 means only current state considered. 2 means current and previous states considered. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakemarkovchainmodellevel'></a>property: Level
#### Summary
 Defines the n-gram unit within the model. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakemarkovchainmodelmatrix'></a>property: Matrix
#### Summary
 The transition matrix. Defines the current states, and the next possible states with the corresponding frequency expressed as a value between 0 and 1. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakemarkovchainmodelserialise'></a>method: Serialise
#### Signature
``` c#
MarkovChainModel.Serialise()
```
#### Summary
 Serialiases the current model to json string. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakemarkovchainmodeldeserialise(systemstring)'></a>method: Deserialise
#### Signature
``` c#
MarkovChainModel.Deserialise(System.String json)
```
#### Summary
 Creates a new MarkovChainModel from a json string. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|json: |The json string.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakemarkovchaintrainer'></a>type: MarkovChainTrainer
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Creates a transition matrix for a given corpus or training input. 

### Type Parameters:
None

>### <a id='dbaronenetfakemarkovchaintrainertrain(systemiostream,dbaronenetfakemarkovchaintrainerconfiguration)'></a>method: Train
#### Signature
``` c#
MarkovChainTrainer.Train(System.IO.Stream stream, Dbarone.Net.Fake.MarkovChainTrainerConfiguration configuration)
```
#### Summary
 Creates a transition matrix for a given corpus or training text. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|stream: |The stream containing the corpus.|
|configuration: |The configuration settings.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakeprocesslinedelegate'></a>type: ProcessLineDelegate
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Delegate for callback function for manipulating the current line prior to it being processed. You can remove any unwanted characters here. 

### Type Parameters:
None


---
>## <a id='dbaronenetfakestochasticmatrix'></a>type: StochasticMatrix
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Represents a transition matrix for a Markov chain model. The public property is a List to allow simpler serialisation / deserialisation. A private dictionary is also implemented to allow faster retrieval of rows by key. 

### Type Parameters:
None

>### <a id='dbaronenetfakestochasticmatrixrows'></a>property: Rows
#### Summary
 The rows in the matrix. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakestochasticmatrixelement'></a>type: StochasticMatrixElement
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Represents the next states for a given starting state in a stochastic model. 

### Type Parameters:
None

>### <a id='dbaronenetfakestochasticmatrixelementnextstate'></a>property: NextState
#### Summary
 The (next) state. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakestochasticmatrixelementoccurences'></a>property: Occurences
#### Summary
 The number of occurences of this value. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakestochasticmatrixelementprobability'></a>property: Probability
#### Summary
 The probability of this value occurring. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakestochasticmatrixrow'></a>type: StochasticMatrixRow
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Represents a row in a transition matrix for a Markov chain model. Each row stores a single current state n-gram, with it's corresponding next values. The sum of the probabilities of the next values equals 1. 

### Type Parameters:
None


---
>## <a id='dbaronenetfakesequencesampler'></a>type: SequenceSampler
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Generates a sequential list of integers. Can generate auto-identity or sequence numbers with naturally occuring gaps. 

### Type Parameters:
None

>### <a id='dbaronenetfakesequencesamplerstart'></a>property: Start
#### Summary
 Starting number. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakesequencesamplerprevious'></a>property: Previous
#### Summary
 Previous number. 


<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakeuniquesampler`1'></a>type: UniqueSampler
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Sampler that guarantees each sampled item to be unique. 

### Type Parameters:
None


---
>## <a id='dbaronenetfakeweighteditem`1'></a>type: WeightedItem
### Namespace:
`Dbarone.Net.Fake`
### Summary
 A weighted item containing a weight and a value. 

### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the item. The type must implement the IWeightedItem interface.|

>### <a id='dbaronenetfakeweighteditem`1value'></a>property: Value
#### Summary
 The value. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeweighteditem`1weight'></a>property: Weight
#### Summary
 The relative weighting or frequency allocated to this value. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetfakeweighteditem`1#ctor(`0,systemdouble)'></a>method: #ctor
#### Signature
``` c#
WeightedItem.#ctor(T value, System.Double weight)
```
#### Summary
 Creates a new WeightedItem instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|value: ||
|weight: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='dbaronenetfakeweightedrandomsampler`1'></a>type: WeightedRandomSampler
### Namespace:
`Dbarone.Net.Fake`
### Summary
 Returns data based on a weighted list of values. 

### Type Parameters:
None


---
>## <a id='exponentialrandom'></a>type: ExponentialRandom
### Namespace:
``
### Summary
 Generates random number for a Poisson process. Each call to Next() returns how much time elapses between consecutive events. https://en.wikipedia.org/wiki/Exponential_distribution https://preshing.com/20111007/how-to-generate-random-timings-for-a-poisson-process/ 

### Type Parameters:
None

>### <a id='exponentialrandomlambda'></a>property: Lambda
#### Summary
 The expected rate of occurence in a given time frame. 


<small>[Back to top](#top)</small>
>### <a id='exponentialrandomnext'></a>method: Next
#### Signature
``` c#
ExponentialRandom.Next()
```
#### Summary
 Returns a random elapsed time between consecutive events. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='poissonrandom'></a>type: PoissonRandom
### Namespace:
``
### Summary
 Generates random Poisson-distributed numbers. Algorithm provided by Knuth. Each random number represents how many events occur in the given period of time, based on the expected rate (Lambda). https://en.wikipedia.org/wiki/Poisson_distribution 

### Type Parameters:
None

>### <a id='poissonrandomlambda'></a>property: Lambda
#### Summary
 The expected rate of occurence in a given time frame. 


<small>[Back to top](#top)</small>
>### <a id='poissonrandomnext'></a>method: Next
#### Signature
``` c#
PoissonRandom.Next()
```
#### Summary
 Generates random Poisson-distributed number. Algorithm attributed to Knuth: https://en.wikipedia.org/wiki/Poisson_distribution#Generating_Poisson-distributed_random_variables 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='addressinfoaddressline1'></a>property: AddressLine1
#### Summary
 Organisational department, sub building name, Apartment / unit / flat number. 


<small>[Back to top](#top)</small>
>### <a id='addressinfoaddressline2'></a>property: AddressLine2
#### Summary
 The building number and street name. 


<small>[Back to top](#top)</small>
>### <a id='addressinfotown'></a>property: Town
#### Summary
 The postal town / city. 


<small>[Back to top](#top)</small>
>### <a id='addressinforegion'></a>property: Region
#### Summary
 The Region. 


<small>[Back to top](#top)</small>
>### <a id='addressinfopostcode'></a>property: Postcode
#### Summary
 The postcode. 


<small>[Back to top](#top)</small>
>### <a id='addressinfocountry'></a>property: Country
#### Summary
 The country. 


<small>[Back to top](#top)</small>
>### <a id='addresssamplertownregion'></a>property: TownRegion
#### Summary
 Cache of regions for each town. Ensures that we maintain a M:1 relationship 


<small>[Back to top](#top)</small>
>### <a id='addresssamplerregioncountry'></a>property: RegionCountry
#### Summary
 Cache of country for each town. Ensures that we maintain a M:1 relationship 


<small>[Back to top](#top)</small>
>### <a id='addresssamplerregionpostcode'></a>property: RegionPostcode
#### Summary
 Postcode comprises 5 digits 'ddddd' 


<small>[Back to top](#top)</small>

---
>## <a id='personsampler'></a>type: PersonSampler
### Namespace:
``
### Summary
 Generates random people. 

### Type Parameters:
None


---
>## <a id='abstractevent`1'></a>type: AbstractEvent
### Namespace:
``
### Summary
 Represents an event with associated data payload. 

### Type Parameters:
|Param | Description |
|-----|-----|
|T: ||


---
>## <a id='ievent'></a>type: IEvent
### Namespace:
``
### Summary
 Interface for an event. 

### Type Parameters:
None


---
>## <a id='markovchainlevel'></a>type: MarkovChainLevel
### Namespace:
``
### Summary
 Defines the unit of each n-gram in the model. 

### Type Parameters:
None

>### <a id='markovchainlevelword'></a>field: Word
#### Summary
 N-gram unit is a word. 


<small>[Back to top](#top)</small>
>### <a id='markovchainlevelcharacter'></a>field: Character
#### Summary
 N-gram unit is a character. 


<small>[Back to top](#top)</small>

---
>## <a id='markovchainsampler'></a>type: MarkovChainSampler
### Namespace:
``
### Summary
 Markov chain text sampler class. Generates pseudo-random text based on a model known as a transition matrix. 

### Type Parameters:
None

>### <a id='markovchainsamplercurrentstate'></a>field: CurrentState
#### Summary
 Stores the current state. 


<small>[Back to top](#top)</small>
>### <a id='markovchainsampler#ctor(dbaronenetfakemarkovchainmodel)'></a>method: #ctor
#### Signature
``` c#
MarkovChainSampler.#ctor(Dbarone.Net.Fake.MarkovChainModel model)
```
#### Summary
 Creates a new MarkovChainSampler instance from a pre-trained model. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|model: |The model to use.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='markovchainsampler#ctor(dbaronenetfakemarkovchainmodel,dbaronenetfakeirandom{systemdouble})'></a>method: #ctor
#### Signature
``` c#
MarkovChainSampler.#ctor(Dbarone.Net.Fake.MarkovChainModel random, Dbarone.Net.Fake.IRandom<System.Double> model)
```
#### Summary
 Creates a new MarkovChainSampler instance from a pre-trained model and a specified random number generator. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|random: |A provided random number generator.|
|model: |The model to use.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
>## <a id='iweighteditem'></a>type: IWeightedItem
### Namespace:
``
### Summary
 Represents a type which can have a weight applied to it to affect its frequency of being selected using a weighted random sampler. 

### Type Parameters:
None

