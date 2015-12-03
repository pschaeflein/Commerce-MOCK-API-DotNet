using System;
using System.IO;
using System.Net.Http;
using System.Web.Configuration;

namespace MSCorp.CrestMockWebApi.Services
{
    public class FileLoggingService
    {
        private static readonly string LogFileName = WebConfigurationManager.AppSettings["LogFileName"];

        public static void LogInformational(HttpRequestMessage request, string trackingId, string correlationId, string sessionId)
        {
            try
            {
                using (StreamWriter w = File.AppendText(LogFileName))
                {
                    w.Write("\r\nLog Entry : ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    w.WriteLine("Uri :{0}", request.RequestUri.AbsoluteUri);
                    w.WriteLine("Message Body :{0}", request.Content.ReadAsStringAsync().Result);
                    w.WriteLine("Tracking Id :{0}", trackingId);
                    w.WriteLine("Correlation Id :{0}", correlationId);
                    w.WriteLine("Session Id :{0}", sessionId);
                    w.WriteLine("----------------------------------------");
                }

            }
            catch (Exception exception)
            {
                string message = exception.Message;
                Console.WriteLine(message);
            }
        }
        
    }
}