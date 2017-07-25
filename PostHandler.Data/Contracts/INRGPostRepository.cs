using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostHandler.Model;

namespace PostHandler.Data.Contracts
{
    public interface INRGPostRepository : IDataRepository<NRGPostHandler>
    {
        Task<int> InsertNRPostDataAsync(NRGPostHandler instance);
    }
}
