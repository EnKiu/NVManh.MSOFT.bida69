using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using MSOFT.Infrastructure.Interfaces;
using MSOFT.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Infrastructure.Repository
{
    public class InventoryCategoryRepository:ADORepository, IInventoryCategoryRepository
    {
       public InventoryCategoryRepository(IDataContext dataContext) : base(dataContext)
        {

        }
    }
}
