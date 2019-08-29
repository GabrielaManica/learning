using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Cadastro.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cadastro.APP.Pages.Pessoas
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string filtroNome { get; set; }

        [BindProperty(SupportsGet = true)]
        public string filtroEmail { get; set; }

        public IList<Pessoa> Pessoas { get; set; }

        public void OnGet()

        {

            var request = (HttpWebRequest)WebRequest.Create($"https://localhost:44395/api/pessoas?nome={filtroNome}&email={filtroEmail}");
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
            Pessoas = JsonConvert.DeserializeObject<List<Pessoa>>(content);
        }
    }
}
