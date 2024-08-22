using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using static BinaryVersion.Program;

namespace BinaryVersion
{
    public class Program
    {
        // Hoyo are weird for adding an empty byte before a new string...
        private static string ReadCustomString(BinaryReader reader)
        {
            reader.ReadByte();
            return reader.ReadString();
        }

        public class BinaryVersionData
        {
            public string Branch { get; set; }
            public uint Revision { get; set; }
            public uint MajorVersion { get; set; }
            public uint MinorVersion { get; set; }
            public uint PatchVersion { get; set; }
            public uint KJLNPMBMHPP { get; set; }
            public uint BEHBCIGKAIA { get; set; }
            public uint DNHBBFJNFIA { get; set; }
            public uint JOGEBFOIHBA { get; set; }
            public uint KAHDPOCKEOK { get; set; }
            public uint DKAEGGDFNCE { get; set; }
            public uint IKGBOMPEEFK { get; set; }
            public uint KIIALDBNPGF { get; set; }
            public uint EAOFFMBMFEB { get; set; }
            public uint PBIENIGLJFK { get; set; }
            public uint FMOFDMGBLKN { get; set; }
            public uint NHLEELPBMPB { get; set; }
            public uint DIECOIPHCFH { get; set; }
            public uint MLBMIOPJKDN { get; set; }
            public uint AIJCAACHKPA { get; set; }
            public string Time { get; set; }
            public string PakType { get; set; }
            public string PakTypeDetail { get; set; }
            public string StartAsset { get; set; }
            public string StartDesignData { get; set; }
            public string DispatchSeed { get; set; }
            public string VersionString { get; set; }
            public string VersionHash { get; set; }
            public uint GameCoreVersion { get; set; }
            public bool IsEnableExcludeAsset { get; set; }
            public string Sdk_PS_Client_Id { get; set; }
        }

        public static void Main(string[] args)
        {

            if (args.Length < 1)
            {
                Console.WriteLine("Usage: .exe <BinaryVersion.bytes path>");
                return;
            }

            var BinaryVersionPath = args[0];

            if (!File.Exists(BinaryVersionPath))
            {
                Console.WriteLine("ERROR: The path doesn't contain BinaryVersion.bytes file!");
                return;
            }

            var BinaryVersionBytes = File.ReadAllBytes(BinaryVersionPath);
            using var ms = new MemoryStream(BinaryVersionBytes);
            using var br = new EndianBinaryReader(ms, Encoding.UTF8);
            BinaryVersionData binaryVersionData = new();

            foreach (var i in typeof(BinaryVersionData).GetProperties())
            {
                switch (i.PropertyType)
                {
                    case Type t when t == typeof(byte):
                        i.SetValue(binaryVersionData, br.ReadByte());
                        break;
                    case Type t when t == typeof(uint):
                        i.SetValue(binaryVersionData, br.ReadUInt32BE());
                        break;
                    case Type t when t == typeof(string):
                        i.SetValue(binaryVersionData, ReadCustomString(br));
                        break;
                    case Type t when t == typeof(bool):
                        i.SetValue(binaryVersionData, br.ReadBoolean());
                        break;
                    default:
                        // should NOT happen
                        throw new NotSupportedException("Stupid happened :(");
                }
            }

            string jsonStr = JsonConvert.SerializeObject(binaryVersionData, Formatting.Indented);
            File.WriteAllText("BinaryVersion.json", jsonStr);
            Console.WriteLine("Parsing complete, the data will be saved in BinaryVersion.json.");
        }
    }
}
