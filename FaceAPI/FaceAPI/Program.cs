using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;

namespace FaceAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //UsePictureURL();
            UseLocalPicture();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// use picture URL
        /// </summary>
        static async void UsePictureURL()

        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4fe95dd8ec4247a69cb2a001efda06c6");//Face API key

            // Request parameters
            queryString["returnFaceId"] = "true";
            queryString["returnFaceLandmarks"] = "false";
            queryString["returnFaceAttributes"] = "age";
            var uri = "https://api.cognitive.azure.cn/face/v1.0/detect?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{\"url\":\"https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1494416315487&di=5e05a310f8c7b3fec011901ff3d13f93&imgtype=0&src=http%3A%2F%2Fimgsrc.baidu.com%2Fbaike%2Fpic%2Fitem%2F4034970a304e251ff1e3819aa486c9177f3e53bf.jpg\"}");//Picture URL

            using (var content = new ByteArrayContent(byteData))
            {
                response = await client.PostAsync(uri, content);
            }

            //response result
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("response:" + result);
        }

        /// <summary>
        /// use local picture
        /// </summary>
        static async void UseLocalPicture()
        {
            var client = new HttpClient();
            
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4fe95dd8ec4247a69cb2a001efda06c6");//Face API key

            // Request parameters
            queryString["returnFaceId"] = "true";
            queryString["returnFaceLandmarks"] = "false";
            queryString["returnFaceAttributes"] = "age";
            var uri = "https://api.cognitive.azure.cn/face/v1.0/detect?" + queryString;

            HttpResponseMessage response;

            //read local picture to byte[]
            string path = @"C:\Users\yuvmtest\Desktop\test.jpg";//local picture path
            FileStream fs = File.OpenRead(path); //OpenRead
            int filelength = 0;
            filelength = (int)fs.Length; 
            byte[] image = new byte[filelength]; 
            fs.Read(image, 0, filelength);
            fs.Close();

            using (var content = new ByteArrayContent(image))
            {
                content.Headers.Add("Content-Type", "application/octet-stream");//set content-type
                response = await client.PostAsync(uri, content);
            }

            //response result
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("response:" + result);
        }
    }
}
