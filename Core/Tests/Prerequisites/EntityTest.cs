using Core.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Core.Tests.Prerequisites
{
    internal class EntityTest : Entity<EntityTest, ModelCoreTest, Guid>, IConvertable<EntityTest, ModelCoreTest, Guid>
    {
        public string? TestProperty { get; set; }

        public ModelCoreTest ConvertEntityToDTO() => new()
        {
            Id = this.Id,
            TestProperty = this.TestProperty
        };

        public static EntityTest ConvertDTOToEntity(ModelCoreTest dto) => new()
        {
            Id = dto.Id,
            TestProperty = dto.TestProperty
        };
    }
}
