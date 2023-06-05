namespace Foni.Code.AssetSystem.TypeConverter
{
    public interface IAssetTypeConverter<out TResultType>
    {
        public TResultType Convert(string rawAssetContent);
    }
}