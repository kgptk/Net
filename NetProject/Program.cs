using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Server("Hello");
        }

        public void task1()
        {
            Message msg = new Message() 
            { 
                Text = "Hello", 
                DateTime = DateTime.Now, 
                NicknameFrom = "Artem",
                NicknameTo = "All"
            };

            string json = msg.SerialazeMassageToJson();
            Console.WriteLine(json);
            Message? msgDeserialized = Message.DeserializeFromJson(json);
        }

        static void Server(string name)
        {
            UdpClient udpClient = new UdpClient(12345);

            //IPEndPoint iPEndPoint = new IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 12345);
            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (true)
            {
                byte[] buffer = udpClient.Receive(ref  iPEndPoint);


                if (buffer != null)
                {

                    var massageText = Encoding.UTF8.GetString(buffer);
                    Message message = Message.DeserializeFromJson(massageText);
                    message.Print();
                }
                else
                {
                    Console.WriteLine("Сообщение не получено!");
                }
                

            }
        }
    }
}
