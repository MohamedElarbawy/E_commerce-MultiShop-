using AutoMapper;
using BusinessLogicLayer;
using CoreLayer;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using MVC_Layer.Models;
using MVC_Layer.Services;
using System.Text.Json;
using X.Paymob.CashIn;

namespace MVC_Layer.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPaymobCashInBroker broker;

        public CheckOutController(IUnitOfWork unitOfWork, IMapper mapper, IPaymobCashInBroker broker)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.broker = broker;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Index(UserDataViewModel userVM)
        {
            if (!ModelState.IsValid)
                return View(userVM);

            if(userVM.itemsInJson != null) { 
            UserData user = mapper.Map<UserData>(userVM);
            Order order = new Order { OrderUser = user };
          
            CartItemViewModel[]? ObjectsFromJason = JsonSerializer.Deserialize<CartItemViewModel[]>(userVM.itemsInJson);
            List<CartItem> itemsList = new();

            if (ObjectsFromJason != null && ObjectsFromJason.Length > 0)
                foreach (var item in ObjectsFromJason)
                {
                    CartItem cartItem = new CartItem { CartItemOrder = order };
                    cartItem.ProductId = int.Parse(item.id);
                    cartItem.Quantity = item.count;
                    cartItem.Size = item.size;
                    if (item.color != null)
                        cartItem.ColorId = unitOfWork.Colors.GetIdByName(item.color);
                    else cartItem.ColorId = unitOfWork.Colors.GetDefaultColorId(int.Parse(item.id));

                    itemsList.Add(cartItem);
                }
            unitOfWork.CartItems.AddRange(itemsList);
                unitOfWork.Complete();
            return RedirectToAction("Payment");
            }
            return View(userVM);
        }
        public async Task<IActionResult> Payment()
        {
            CashInService cashInService = new(broker);
           string s= await cashInService.RequestCardPaymentKey();
            Console.WriteLine(s);
            return Redirect(s);
        }
   

    }
}
