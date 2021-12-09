using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Semprg_StringFormatting
{
    class Program
    {
        private const string dataUrl =
            "https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt";
        static async Task Main(string[] args)
        {
            string data = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                data = await client.GetStringAsync(dataUrl);
            }
            
            //date
            //země|měna|množství|kód|kurz
            //austrálie|dolar|1|aud|16,061 -> 
            //Australie \t dolar \t 16,077 CZK / 1 AUD

            string[] lines = data.Split('\n');
            StringBuilder builder = new StringBuilder();
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                string[] parts = line.Split('|');
                if (parts.Length < 4)
                {
                    continue;
                }
                string resLine = $"{parts[0].PadRight(20)}{parts[1].PadRight(15)}{parts[4].PadRight(6)} CZK / {parts[2]} {parts[3]}";
                builder.Append(resLine).Append('\n');
                if (i == 1)
                {
                    builder.Append('\n');
                }
            }

            Console.WriteLine(builder.ToString());
            Console.ReadLine();
        }
    }
}
