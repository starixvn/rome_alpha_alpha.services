using Fanex.Data.Repository;

namespace Alpha.Customers.Criteria;

public class GetDomainsCriteria : CriteriaBase
{
    public GetDomainsCriteria(string site)
        => Site = site;

    public string Site { get; }

    public override string GetSettingKey()
       => "Age_DomainListSel";

    public override bool IsValid()
        => !string.IsNullOrWhiteSpace(Site);
}
