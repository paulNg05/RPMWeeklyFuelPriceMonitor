
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPMFuelDataManager.Library.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            this._config = config;
        }

        public string GetConnectionString(string name)
        {
         
           return _config.GetConnectionString(name);
        }

        //Generic SaveData method with stroe procedure
        public void SaveData<T>(string sp_storeProcedureName, T parameters, string connetionStringName)
        {
            string connectionString = GetConnectionString(connetionStringName);

            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Execute(sp_storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Generic loaddata method with store procedure
        public List<T> LoadData<T, U>(string sp_storeProcedureName, U parameters, string ConnectionStringName)
        {
            string connectionString = GetConnectionString(ConnectionStringName);
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                List<T> rowResults = cnn.Query<T>(sp_storeProcedureName, parameters, commandType: CommandType.StoredProcedure).ToList();
                return rowResults;
            }
        }

    }
}
