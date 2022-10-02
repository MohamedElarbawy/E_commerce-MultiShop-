﻿namespace BusinessLogicLayer.Helper
{
    public class Pager
    {
        public int TotalItems { get;private set; }
        public int PageSize { get;private set; }
        public int TotalPages { get;private set; }
        public int StartPage { get;private set; }
        public int CurrentPage { get;private set; }
        public int EndPage { get;private set; }

        public Pager()
        {

        }
        public Pager(int totalItems,int pageNumber ,int pageSize=20)
        {
            if(pageSize<=0)
                pageSize = 20;
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = pageNumber;

            int startPage=currentPage-5;
            int endPage=currentPage+4;

            if (startPage <= 0)
            {
                endPage-=(startPage-1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                    startPage = endPage - 9;
            }

            TotalItems = totalItems;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            CurrentPage = currentPage;

        }






    }
}
