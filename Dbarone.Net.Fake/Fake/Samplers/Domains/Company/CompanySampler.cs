using Dbarone.Net.Fake;

/// <summary>
/// Generates random company names based on a stochastic model.
/// </summary>
public class CompanySampler : AbstractSampler<string>, ISampler<string>
{
    MarkovChainSampler CompanyNameSampler { get; set; }

    #region Ctor

    public CompanySampler() : base()
    {
        var stochasticModelCompanies = Dataset.GetString(DatasetEnum.Stochastic_Model_Company);
        var companyModel = MarkovChainModel.Deserialise(stochasticModelCompanies);
        CompanyNameSampler = new MarkovChainSampler(companyModel);
    }

    public CompanySampler(IRandom<double> random) : base(random)
    {
        var stochasticModelCompanies = Dataset.GetString(DatasetEnum.Stochastic_Model_Company);
        var companyModel = MarkovChainModel.Deserialise(stochasticModelCompanies);
        CompanyNameSampler = new MarkovChainSampler(companyModel);
    }

    #endregion

    #region Methods

    public override string Next()
    {
        return CompanyNameSampler.Next();
    }

    #endregion
}