using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace SimpleWebServer
{
    public class WebServer
    {
        private int port = 55555;

        private string host = "*"; // try loaclhost and 127.0.0.1

        private TcpListener listener;

        public WebServer()
        {
            if (listener != null && listener.Server.IsBound)
                throw new Exception("Error 1: Cannot run the server at this moment");

            IPAddress IP = (host == "*") ? IPAddress.Any : IPAddress.Parse(host);

            listener = new TcpListener(IP, port);
            listener.Start();
            Console.WriteLine($"Server Started ....");
            try
            {
                while (true)
                {
                    var threadChild = new Thread(process);
                    threadChild.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void process()
        {
            Socket client = listener.AcceptSocket();
            Console.WriteLine("Listening for connection.");

            byte[] data = new byte[1000];
            int size = client.Receive(data);
            Console.WriteLine("viewing data as array of ASCII characters: ");

            for (int i = 0; i < size; i++)
                Console.Write(Convert.ToChar(data[i]));

            Console.WriteLine("\n\n========================================================\n");
            client.Close();
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Hello, Http server!");
                var x = new WebServer();
            }
        }
    }
}