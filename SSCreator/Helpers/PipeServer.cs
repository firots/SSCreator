using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace SSCreator {
    public class PipeServer {
        private static int numThreads = 4;

        public static void Run() {
            Thread[] servers = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++) {
                servers[i] = new Thread(ServerThread);
                servers[i].Start();
            }
        }

        private static void ServerThread(object data) {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("sscreatorpipe", PipeDirection.InOut, numThreads);
            int threadId = Thread.CurrentThread.ManagedThreadId;

            while(true) {
                pipeServer.WaitForConnection();
                Console.WriteLine("Client connected on thread[{0}].", threadId);
                try {
                    StreamString ss = new StreamString(pipeServer);
                    ss.WriteString("waiting for json");
                    string json = ss.ReadString();
                    PipeParser.parse(json, ss);
                }
                catch (IOException e) {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }
                pipeServer.Disconnect();
            }
        }
    }

    public class StreamString {
        private Stream ioStream;
        private UnicodeEncoding streamEncoding;

        public StreamString(Stream ioStream) {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public string ReadString() {
            int len = 0;
            len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            return streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString) {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue) {
                len = (int)UInt16.MaxValue;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }
}
