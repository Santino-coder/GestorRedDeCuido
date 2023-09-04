using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.DA
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> opciones) : base(opciones) { }
    }
}
