using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ZeroRootKillerServer
{
    internal class ServerClass
    {
        public static void Run()
        {
            ServerStart();
        }
        static int port = 8666; // порт для приема входящих запросов
        static IPAddress? ip_address;
        static IPAddress GetMyIp()
        {
            using (WebClient client = new WebClient())
            {
                List<String> hosts = new List<String>();
                hosts.Add("https://icanhazip.com");
                hosts.Add("https://api.ipify.org");
                hosts.Add("https://ipinfo.io/ip");
                hosts.Add("https://wtfismyip.com/text");
                hosts.Add("https://checkip.amazonaws.com/");
                hosts.Add("https://bot.whatismyipaddress.com/");
                hosts.Add("https://ipecho.net/plain");
                foreach (String host in hosts)
                {
                    try
                    {
                        String ipAdressString = client.DownloadString(host);
                        ipAdressString = ipAdressString.Replace("\n", "");
                        return IPAddress.Parse(ipAdressString);
                    }
                    catch
                    {
                    }
                }
            }
            return IPAddress.Parse("127.0.0.1");
        }
        static void init()
        {
            IPAddress ip_address = GetMyIp();
        }
        static void ServerStart()
        {
            init();
            // получаем адреса для запуска сокета

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                    // отправляем ответ
                    string message = "ваше сообщение доставлено";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
