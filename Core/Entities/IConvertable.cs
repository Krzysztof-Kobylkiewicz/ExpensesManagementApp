using Core.Models;

namespace Core.Entities
{
    public interface IConvertable<E, DTO, Key> where E : Entity<E, DTO, Key> where DTO : IModelCore<Key> where Key : IEquatable<Key>
    {
        virtual DTO ConvertEntityToDTO() => throw new NotImplementedException("Entity does not have its own implementation of converting to DTO.");
        static virtual E ConvertDTOToEntity(DTO dto) => throw new NotImplementedException("DTO does not have its own implementation of converting to entity.");
    }
}
