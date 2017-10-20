using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace XboxStarter
{
    public static class XboxOn
    {
        public static int SendAttempts { get; set; } = 5;

        public static String IpAddress { get; private set; } = "";
        public static int Port { get; private set; } = 5050;

        public static String LiveId { get; private set; } = "";

        public static IPEndPoint Endpoint
        {
            get
            {
                return new IPEndPoint(IPAddress.Parse(IpAddress), Port);
            }
        }

        public static void PowerOnXboxOne()
        {
            const String XBOX_POWER = "dd02001300000010";

            var endPoint = new IPEndPoint(IPAddress.Any, 0);


            var s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                Blocking = false
            };
            s.Bind(endPoint);

            s.Connect(Endpoint);

            var power_packet = StringToByteArray(XBOX_POWER).Concat(Encoding.UTF8.GetBytes(LiveId + "\x00"));


            for (int x = 0; x < SendAttempts; x++)
            {
                s.Send(power_packet.ToArray());
                //Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }


        }

        public static async void PowerOnXboxOneAsync()
        {
            await Task.Run(()=> 
            {
                PowerOnXboxOne();
            });
        }

        public static void ImportSettings(String Ip, String LiveId, int SendAttempts = 5, int Port = 5050)
        {
            IpAddress = Ip;
            XboxOn.LiveId = LiveId;
            XboxOn.Port = Port;
            XboxOn.SendAttempts = SendAttempts;
        }

        private static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
