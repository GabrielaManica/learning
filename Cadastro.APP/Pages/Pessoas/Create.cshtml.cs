using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cadastro.Model;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Cadastro.APP.Pages.Pessoas
{
    public class CreateModel : PageModel
    {

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Pessoa pessoa { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var request = (HttpWebRequest)WebRequest.Create("https://localhost:44395/api/pessoas");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(pessoa);

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(serializedObject);
            }

            var response = (HttpWebResponse)request.GetResponse();
            string content = string.Empty;
            using (var stream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }

            //https://stackoverflow.com/questions/4611031/convert-json-string-to-c-sharp-object
            pessoa = JsonConvert.DeserializeObject<Pessoa>(content);
            return RedirectToPage("./Index");
        }
    }
}