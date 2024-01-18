using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject009.Models;
using MiniProject009.SM;

namespace MiniProject009.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly Context _context;

        public StatisticsController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowExpendituresByCategory()
        {
            return View();
        }

        [HttpPost]
        public List<object> ExpendituresByCategoryForChart()
        {
            var result =
                (
                    from e in _context.Expenditures
                    join c in _context.Categories
                    on e.CategoryId equals c.Id
                    group new { e, c } by new { c.CategoryName } into q
                    select new ExpendituresByCategory()
                    {
                        CategoryName = q.Key.CategoryName,
                        Total = q.Sum(x => x.e.ExpenditureAmount)
                    }
                ).ToList();
            List<object> data = new List<object>();
            List<string> labels = result.Select(p => p.CategoryName).ToList();
            data.Add(labels);
            List<decimal> SalesNumber = result.Select(p => p.Total).ToList();
            data.Add(SalesNumber);
            return data;
        }

        public IActionResult ShowExpendituresByCategoryInTable()
        {
            var result =
                (
                    from e in _context.Expenditures
                    join c in _context.Categories
                    on e.CategoryId equals c.Id
                    group new { e, c } by new { c.CategoryName } into q
                    select new ExpendituresByCategory()
                    {
                        CategoryName = q.Key.CategoryName,
                        Total = q.Sum(x => x.e.ExpenditureAmount)
                    }
                ).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult ShowExpendituresByCategoryInTableFilterByMonth()
        {
            var list_yearMonth = _context.Expenditures.OrderByDescending(a => a.ExpenditureDate).Select(y => new { ExpenditureDate = ConvertToYearMonth(y.ExpenditureDate) });
            ViewData["YearMonth"] = new SelectList(list_yearMonth, "ExpenditureDate", "ExpenditureDate");

            string criteria = list_yearMonth.ToList()[0].ToString();
            int first = criteria.IndexOf("=");
            criteria = criteria.Substring(first + 2, 7);

            var result = _context.Expenditures
                .AsEnumerable()
                .Where(c => ConvertToYearMonth(c.ExpenditureDate) == criteria)
                .Join(_context.Categories, e => e.CategoryId, c => c.Id, (e, c) => new ExpendituresByCategory()
                {
                    CategoryName = c.CategoryName,
                    Total = e.ExpenditureAmount
                })
                .GroupBy(y => y.CategoryName).Select(z => new ExpendituresByCategory()
                {
                    CategoryName = z.Key,
                    Total = z.Sum(x => x.Total)
                });

            return View(result);
        }

        [HttpPost]
        public IActionResult ShowExpendituresByCategoryInTableFilterByMonth(string yearMonth)
        {
            var list_yearMonth = _context.Expenditures.OrderByDescending(a => a.ExpenditureDate).Select(y => new { ExpenditureDate = ConvertToYearMonth(y.ExpenditureDate) });
            ViewData["YearMonth"] = new SelectList(list_yearMonth, "ExpenditureDate", "ExpenditureDate");

            string criteria = yearMonth;

            var result = _context.Expenditures
                .AsEnumerable()
                .Where(c => ConvertToYearMonth(c.ExpenditureDate) == criteria)
                .Join(_context.Categories, e => e.CategoryId, c => c.Id, (e, c) => new ExpendituresByCategory()
                {
                    CategoryName = c.CategoryName,
                    Total = e.ExpenditureAmount
                })
                .GroupBy(y => y.CategoryName).Select(z => new ExpendituresByCategory()
                {
                    CategoryName = z.Key,
                    Total = z.Sum(x => x.Total)
                });
            ViewData["WhichYearMonth"] = criteria;

            return View(result);
        }

        public static string ConvertToYearMonth(DateOnly myDate)
        {
            return myDate.ToString("yyyy-MM");
        }
    }
}
