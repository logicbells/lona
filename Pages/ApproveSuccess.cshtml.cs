using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lona.Pages;

public class ApproveSuccessModel : PageModel
{
    private readonly ILogger<ApproveSuccessModel> _logger;

    public ApproveSuccessModel(ILogger<ApproveSuccessModel> logger)
    {
        _logger = logger;
    }

    public string Message { get; set; }

   
    public void OnGet()
    {
        this.Message = "This is my First ASP.Net Core Razor Page";
    }
}



