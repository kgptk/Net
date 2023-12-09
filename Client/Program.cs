using NetServer;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            

            SentMessage(args[0], args[1]);
        }

        public static void SentMessage(string From, string ip)
        {
            

            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);

            while (true)
            {
                string messageText;

                do
                {
                    //Console.Clear();
                    Console.WriteLine("Введите сообщение");
                    messageText = Console.ReadLine();

                } while (string.IsNullOrEmpty(messageText));
               
                Message message = new Message()
                {
                    Text = messageText,
                    NicknameFrom = From,
                    NicknameTo = "Server",
                    DateTime = DateTime.Now
                };
                string json = message.SerialazeMassageToJson();

                byte[] data = Encoding.UTF8.GetBytes(json);
                
                
                int count = udpClient.Send(data, data.Length, iPEndPoint);
                if (count == data.Length) 
                { 
                    Console.WriteLine($"На сервер отправлено {count} байт!"); 
                }
                else
                {
                    Console.WriteLine("Что-то пошло не так!");
                }
               
          

            }
            



        }
        
    }
}
