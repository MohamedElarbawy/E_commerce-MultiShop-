using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
    public interface IColorRepo:IGenericRepository<Color>
    {

        public IEnumerable<Color> GetColors(List<int> ids);
        public int GetIdByName(string name);
        public int GetDefaultColorId(int ProductId);


    }
}
