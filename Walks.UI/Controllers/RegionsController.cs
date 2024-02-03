using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using Walks.UI.Models;
using Walks.UI.Models.DTO;

namespace Walks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                //Get all regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7069/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                //var stringResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

                ViewBag.Response = response;
            }
            catch (Exception ex)
            {
                //Log the exception
                //throw;
            }


            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7069/api/regions"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7069/api/regions/{id.ToString()}");
            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7069/api/regions/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Delete(RegionDto request)
        //{
        //    try
        //    {
        //        var client = httpClientFactory.CreateClient();
        //        var httpResponseMessage = await client.DeleteAsync($"https://localhost:7069/api/regions/{request.Id}");
        //        httpResponseMessage.EnsureSuccessStatusCode();
        //        return RedirectToAction("Index", "Regions");
        //    }
        //    catch (Exception ex)
        //    {

        //        //Console message
        //    }

        //    return View("Edit");
        //}
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Guid id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7069/api/regions/{id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                // Console.WriteLine(ex.Message);
            }

            return View("Edit");
        }

        [HttpGet]
        public async Task<IActionResult> Patch(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7069/api/regions/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Patch(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();           
            var originalRegionResponse = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7069/api/regions/{request.Id.ToString()}");
            var patchDocument = new JsonPatchDocument<PatchRegionRequestDto>();

            // Compare properties and add operations to the patch document
            if (request.Code != originalRegionResponse.Code)
            {
                patchDocument.Replace(x => x.Code, request.Code);
            }

            if (request.Name != originalRegionResponse.Name)
            {
                patchDocument.Replace(x => x.Name, request.Name);
            }

            if (request.RegionImageUrl != originalRegionResponse.RegionImageUrl)
            {
                patchDocument.Replace(x => x.RegionImageUrl, request.RegionImageUrl);
            }

            string patchJson = JsonConvert.SerializeObject(patchDocument);
            var content = new StringContent(patchJson, Encoding.UTF8, "application/json-patch+json");
            var httpResponseMessage2 = await client.PatchAsync($"https://localhost:7069/api/regions/{request.Id}", content);

            httpResponseMessage2.EnsureSuccessStatusCode();
            var responseContent = await httpResponseMessage2.Content.ReadAsStringAsync();

            if (responseContent is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

    }
}
