namespace Caterpillar.Core.Common
{
    /// <summary>
    /// Maps implemented type to type of T
    /// </summary>
    /// <typeparam name="MapTo">Maps a type to type of 'MapTo'</typeparam>
    public interface ITypeMapper<MapTo>
    {
        MapTo ToType();
    }
}
