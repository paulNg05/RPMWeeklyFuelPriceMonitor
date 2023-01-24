using RPMFuelDataManager.Library.Models;

namespace RPMFuelDataManager.Library.DataAccess
{
    public interface IFuelDataExtract
    {
        List<DataModel> GetFuelDat();
        void SaveFuelData(List<DataModel> myData);
    }
}