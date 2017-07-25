using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PostHandler.Data.Contracts;
using PostHandler.Foundation.Helper;
using PostHandler.Model;

namespace PostHandler.Data.Repositories
{
    public class NRGPostRepository : DataRepositoryBase<NRGPostHandler>, INRGPostRepository
    {
        public NRGPostRepository()
        {

        }
        public NRGPostRepository(string connectionString)
            : base(connectionString)
        {

        }


        #region Factory Implementation

        public static NRGPostRepository Create()
        {
            return new NRGPostRepository();
        }

        public static NRGPostRepository Create(string connectionString)
        {
            return new NRGPostRepository(connectionString);
        }

        #endregion

        public async Task<int> InsertNRPostDataAsync(NRGPostHandler data)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@pBTN", data.CustomerNumber);
                p.Add("@pFirstName", data.FirstName);
                p.Add("@pLastName", data.LastName);
                p.Add("@pEmail", data.Email);
                p.Add("@pAddress", data.Address);
                p.Add("@pZip", data.Zip);
                p.Add("@pState", data.State);
                p.Add("@pCity", data.city);
                
                p.Add("@oRetVal", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await ProcessAsync<bool>(NRGPostHandler.StoredProcedure.InsertNRGPostedData, p, commandType: CommandType.StoredProcedure);
                DapperProcedureResult status = (DapperProcedureResult)((int)p.Get<int>("@oRetVal"));
                switch (status)
                {
                    case DapperProcedureResult.Success: return 101;
                    case DapperProcedureResult.Failure: return 2001;
                    default: return 2001;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
