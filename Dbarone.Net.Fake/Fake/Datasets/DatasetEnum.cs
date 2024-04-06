/// <summary>
/// List of datasets stored as resources in the assembly.
/// </summary>
public enum DatasetEnum
{
    /// <summary>
    /// Boys names. UK Office of National Statistics 2021 names.
    /// https://www.ons.gov.uk/peoplepopulationandcommunity/birthsdeathsandmarriages/livebirths/datasets/babynamesinenglandandwalesfrom1996
    /// </summary>
    en_GB_Names_Boy,

    /// <summary>
    /// Girls names. UK Office of National Statistics 2021 names.
    /// https://www.ons.gov.uk/peoplepopulationandcommunity/birthsdeathsandmarriages/livebirths/datasets/babynamesinenglandandwalesfrom1996
    /// </summary>
    en_GB_Names_Girl,

    /// <summary>
    /// Surnames from the US 2010 census (top 1000 names).
    /// https://www.census.gov/topics/population/genealogy/data/2010_surnames.html
    /// </summary>
    Surnames_US_Census_2010,

    /// <summary>
    /// Single-word street names. Based on http://data.gov.uk/dataset/os-locator (https://greem.co.uk/os/os_locator_streets.txt). Data as at 2010.
    /// names under 10 hits excluded.
    /// </summary>
    en_GB_Street_Names_Simple,

    /// <summary>
    /// Dual-word street names. Based on http://data.gov.uk/dataset/os-locator (https://greem.co.uk/os/os_locator_streets.txt). Data as at 2010.
    /// names under 10 hits excluded.
    /// </summary>
    en_GB_Street_Names_Compound,

    /// <summary>
    /// Street types / suffixes. Based on http://data.gov.uk/dataset/os-locator (https://greem.co.uk/os/os_locator_streets.txt). Data as at 2010.
    /// names under 10 hits excluded.
    /// </summary>
    en_GB_Street_Types,

    /// <summary>
    /// Stochastic model that can be used to generate words that sound like UK post towns.
    /// </summary>
    Stochastic_Model_Town,

    /// <summary>
    /// Stochastic model that can be used to generate words that sound like UK regions.
    /// </summary>
    Stochastic_Model_Region,

    /// <summary>
    /// Stochastic model that can be used to generate words that sound like latin words (based on Lorem Ipsum corpus).
    /// </summary>
    Stochastic_Model_Lorem_Ipsum
}