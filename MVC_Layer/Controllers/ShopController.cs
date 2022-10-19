using CoreLayer;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Layer.Controllers
{
    public class ShopController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ShopController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
       
    }
}
