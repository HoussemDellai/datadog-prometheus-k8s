using System.Threading.Tasks;
using Datadog.Trace;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace MvcApp.Controllers
{
    public class TracingDemoController : Controller
    {
        ITracer _tracer;

        public TracingDemoController(ITracer tracer)
        {
            _tracer = tracer;
        }

        public async Task<IActionResult> Index()
        {
            using (var scope = Tracer.Instance.StartActive("tracingDemo_index.sortorders"))
            {
                scope.Span.SetTag("orders_number", "280");

                await SortOrders();
            }
            return View();
        }

        private async Task SortOrders()
        {
            await Task.Delay(500);
        }
    }
}
