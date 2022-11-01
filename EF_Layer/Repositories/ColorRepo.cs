using CoreLayer.Entities;
using CoreLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class ColorRepo : GenericRepository<Color>, IColorRepo
    {
        private readonly MultiShopContext context;

        public ColorRepo(MultiShopContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Color> GetColors(List<int> ids)
        {
            if (ids != null && ids.Count != 0)
            {
                List<Color> colors = new List<Color>();
                foreach (var id in ids)
                {
                    var color = context.Colors.Find(id);
                    if(color!=null)
                    colors.Add(color);
                }

                return colors;
            }
            return Enumerable.Empty<Color>();
        }


        public int GetIdByName(string name)
        {
            return context.Colors.Where(c=>c.ColorName == name).Select(c=>c.Id).FirstOrDefault();
        }


        public int GetDefaultColorId(int ProductId)
        {
            return context.Products.Include(p => p.Colors)
                                    .Where(p => p.Id == ProductId)
                                    .FirstOrDefault().Colors
                                    .Select(c => c.Id)
                                    .FirstOrDefault();


        }
    }
}
