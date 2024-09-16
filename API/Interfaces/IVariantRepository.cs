using API.Entities;

namespace API.Interfaces
{
    public interface IVariantRepository
    {
        void AddVariant(Variant variant);
        void UpdateVariant(Variant variant);
        void DeleteVariant(Variant variant);


    }
}