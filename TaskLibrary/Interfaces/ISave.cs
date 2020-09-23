namespace TaskLibrary.Interfaces
{
    public interface ISave
    {
        ISave SetFileName(string fileName);
        bool Save(byte[] data);
    }
}
