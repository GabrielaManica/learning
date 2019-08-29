using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cadastro.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cadastro.APP.Pages.Pessoas
{
    public class EditModel : PageModel
    {

        [BindProperty]
        public Pessoa pessoa { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = (HttpWebRequest)WebRequest.Create($"https://localhost:44395/api/pessoas/{id}");
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
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

            if (pessoa == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var request = (HttpWebRequest)WebRequest.Create($"https://localhost:44395/api/pessoas/{pessoa.Id}");
            request.Method = "PUT";
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

            return Page();  
        } 
    }
}
