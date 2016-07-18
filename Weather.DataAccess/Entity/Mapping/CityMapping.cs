using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Weather.DataAccess.Entity.Mapping
{
    public class CityMapping : EntityTypeConfiguration<City>
    {
        public CityMapping()
        {
            HasOptional(c => c.Detail)
                .WithRequired(cd => cd.City)
                .WillCascadeOnDelete(true);
        }
    }
}