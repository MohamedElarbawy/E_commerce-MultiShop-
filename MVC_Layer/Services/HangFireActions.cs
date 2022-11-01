
using CoreLayer;


namespace MVC_Layer.Services
{
    public class HangFireActions
    {
        private readonly IUnitOfWork unitOfWork;

        public HangFireActions(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        public void DeleteDiscountCodeAfterExpired()
        {
            var timeNow=DateTime.Now;
           var expiredCodes= unitOfWork.Discounts.GetAllThatMatchesACriteria(d => d.EndDate >= timeNow).ToList();
            if (expiredCodes.Any())
            {
                unitOfWork.Discounts.DeleteRange(expiredCodes);
                unitOfWork.Complete();
                Console.WriteLine("expired coupons have been deleted");
            }

        }


    }
}
