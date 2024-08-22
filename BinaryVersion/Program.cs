using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

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
            public byte Idk { get; set; }
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

            var data = new BinaryVersionData
            {
                Idk = br.ReadByte(),
                Branch = br.ReadString(),
                Revision = br.ReadUInt32BE(),
                MajorVersion = br.ReadUInt32BE(),
                MinorVersion = br.ReadUInt32BE(),
                PatchVersion = br.ReadUInt32BE(),
                KJLNPMBMHPP = br.ReadUInt32BE(),
                BEHBCIGKAIA = br.ReadUInt32BE(),
                DNHBBFJNFIA = br.ReadUInt32BE(),
                JOGEBFOIHBA = br.ReadUInt32BE(),
                KAHDPOCKEOK = br.ReadUInt32BE(),
                DKAEGGDFNCE = br.ReadUInt32BE(),
                IKGBOMPEEFK = br.ReadUInt32BE(),
                KIIALDBNPGF = br.ReadUInt32BE(),
                EAOFFMBMFEB = br.ReadUInt32BE(),
                PBIENIGLJFK = br.ReadUInt32BE(),
                FMOFDMGBLKN = br.ReadUInt32BE(),
                NHLEELPBMPB = br.ReadUInt32BE(),
                DIECOIPHCFH = br.ReadUInt32BE(),
                MLBMIOPJKDN = br.ReadUInt32BE(),
                AIJCAACHKPA = br.ReadUInt32BE(),
                Time = ReadCustomString(br),
                PakType = ReadCustomString(br),
                PakTypeDetail = ReadCustomString(br),
                StartAsset = ReadCustomString(br),
                StartDesignData = ReadCustomString(br),
                DispatchSeed = ReadCustomString(br),
                VersionString = ReadCustomString(br),
                VersionHash = ReadCustomString(br),
                GameCoreVersion = br.ReadUInt32BE(),
                IsEnableExcludeAsset = br.ReadBoolean(),
                Sdk_PS_Client_Id = ReadCustomString(br)
            };

            string jsonStr = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText("BinaryVersion.json", jsonStr);
            Console.WriteLine("Parsing complete, the data will be saved in BinaryVersion.json.");
        }
    }
}
