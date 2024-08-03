using Microsoft.AspNetCore.Mvc;
using LibraryBook.Application.Common.Interfaces;
using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LibraryBook.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;            
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetTotalBookingRadialChartData()
        {
            return Json(await _dashboardService.GetTotalBookingRadialChartData());
        }
        public async Task<IActionResult> GetRegisteredUserChartData()
        {
            return Json(await _dashboardService.GetRegisteredUserChartData());
        }
        public async Task<IActionResult> GetRevenueChartData()
        {

           

            return Json(await _dashboardService.GetRevenueChartData());
        }
        public async Task<IActionResult> GetBookingPieChartData()
        {
          

            return Json(await _dashboardService.GetBookingPieChartData());
        }
        public async Task<IActionResult> GetMemberAndBookingLineChartData()
        {
            return Json(await _dashboardService.GetMemberAndBookingLineChartData());
        }
       
    }
}
