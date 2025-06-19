using Core.Models;

namespace Core.Tests.Prerequisites
{
    internal class ModelCoreTest : ModelCore<ModelCoreTest, Guid>
    {
        public string? TestProperty { get; set; }

        public override bool Equals(ModelCoreTest other)
        {
            if (!Id.Equals(other.Id))
                return false;

            if (TestProperty?.Equals(other.TestProperty) == false)
                return false;

            return true;
        }
    }
}
