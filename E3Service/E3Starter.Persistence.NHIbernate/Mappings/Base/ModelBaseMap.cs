using E3Starter.Models.Base;
using FluentNHibernate.Mapping;

namespace E3Starter.Persistence.NHIbernate.Mappings.Base;

public abstract class ModelBaseMap<T> : ClassMap<T> where T : ModelBase
{
    public ModelBaseMap(string tableName)
    {
        Id(x => x.Id);
        Table(tableName);
    }
}
