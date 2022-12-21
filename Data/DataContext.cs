using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cshap_basic_vscode.Data
{
    public class DataContext : DbContext // thừa kế từ EntityFrameworkCore
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){
            // conent string ở appsetting 
        }

        public DbSet<Character> Characters  =>  Set<Character>();      // {get;set;}
    }
}