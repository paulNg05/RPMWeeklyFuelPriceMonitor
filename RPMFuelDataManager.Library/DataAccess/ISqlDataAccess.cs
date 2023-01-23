namespace RPMFuelDataManager.Library.DataAccess
{
    public interface ISqlDataAccess
    {
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string sp_storeProcedureName, U parameters, string ConnectionStringName);
        void SaveData<T>(string sp_storeProcedureName, T parameters, string connetionStringName);
    }
}