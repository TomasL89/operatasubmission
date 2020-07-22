using System.Threading.Tasks;
using LogSubmissionPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogSubmissionPortal.Controllers
{
    public class LogIssueController : Controller
    {
        // GET: LogIssueController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SuccessfulLogSubmission()
        {
            return View();
        }

        public IActionResult FailedLogSubmission()
        {
            return View();
        }

        // POST: LogIssueController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Log log)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(log);

                var couldUpload = await AWSConnection.UploadLogFileAsync(log);

                if (couldUpload)
                    return RedirectToAction(nameof(SuccessfulLogSubmission));
            }
            catch
            {
                RedirectToAction(nameof(FailedLogSubmission));
            }

            return RedirectToAction(nameof(FailedLogSubmission));
        }
    }
}