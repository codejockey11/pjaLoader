using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using aviationLib;

namespace parachuteLoader
{
    class Program
    {
        static Char[] recordType_001_04 = new Char[04];
        static Char[] pjaId_005_06 = new Char[06];

        static Char[] navaid_011_04 = new Char[04];
        static Char[] azimuth_042_06 = new Char[06];
        static Char[] distance_048_08 = new Char[08];
        static Char[] state_086_02 = new Char[02];
        static Char[] latitude_148_14 = new Char[14];
        static Char[] longitude_174_15 = new Char[15];
        static Char[] airportName_201_50 = new Char[50];
        static Char[] dropZoneName_262_50 = new Char[50];
        static Char[] altitude_312_08 = new Char[08];
        static Char[] radius_320_05 = new Char[05];
        static Char[] descriptive_331_100 = new Char[100];
        static Char[] fssIdent_431_04 = new Char[04];
        static Char[] pjaUse_465_08 = new Char[08];

        static Char[] times_011_75 = new Char[75];

        static Char[] userGroupName_011_75 = new Char[75];
        static Char[] description_086_75 = new Char[75];

        static Char[] contactFacilityId_011_04 = new Char[04];
        static Char[] commercialFrequency_067_08 = new Char[08];
        static Char[] militaryFrequency_076_08 = new Char[08];

        static Char[] remark_011_300 = new Char[300];

        static StreamWriter ofilePJA1 = new StreamWriter("pjalocation.txt");
        static StreamWriter ofilePJA2 = new StreamWriter("pjatimes.txt");
        static StreamWriter ofilePJA3 = new StreamWriter("pjausergroup.txt");
        static StreamWriter ofilePJA4 = new StreamWriter("pjacontact.txt");
        static StreamWriter ofilePJA5 = new StreamWriter("pjaremarks.txt");

        static void Main(String[] args)
        {
            String userprofileFolder = Environment.GetEnvironmentVariable("USERPROFILE");
            String[] fileEntries = Directory.GetFiles(userprofileFolder + "\\Downloads\\", "28DaySubscription*.zip");

            ZipArchive archive = ZipFile.OpenRead(fileEntries[0]);
            ZipArchiveEntry entry = archive.GetEntry("PJA.txt");
            entry.ExtractToFile("PJA.txt", true);

            StreamReader file = new StreamReader("PJA.txt");

            String rec = file.ReadLine();

            while (!file.EndOfStream)
            {
                ProcessRecord(rec);

                rec = file.ReadLine();
            }

            ProcessRecord(rec);

            file.Close();

            ofilePJA1.Close();
            ofilePJA2.Close();
            ofilePJA3.Close();
            ofilePJA4.Close();
            ofilePJA5.Close();
        }

        static void ProcessRecord(String record)
        {
            recordType_001_04 = record.ToCharArray(0, 4);

            String rt = new String(recordType_001_04);
            Int32 r = String.Compare(rt, "PJA1");
            if (r == 0)
            {
                pjaId_005_06 = record.ToCharArray(4, 6);
                String s = new String(pjaId_005_06).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                navaid_011_04 = record.ToCharArray(10, 4);
                s = new String(navaid_011_04).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                azimuth_042_06 = record.ToCharArray(41, 6);
                s = new String(azimuth_042_06).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                distance_048_08 = record.ToCharArray(47, 8);
                s = new String(distance_048_08).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                state_086_02 = record.ToCharArray(85, 2);
                s = new String(state_086_02).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                latitude_148_14 = record.ToCharArray(147, 14);
                longitude_174_15 = record.ToCharArray(173, 15);

                LatLon ll = new LatLon(new String(latitude_148_14).Trim(), new String(longitude_174_15).Trim());
                ofilePJA1.Write(ll.formattedLat);
                ofilePJA1.Write('~');

                ofilePJA1.Write(ll.formattedLon);
                ofilePJA1.Write('~');

                airportName_201_50 = record.ToCharArray(200, 50);
                s = new String(airportName_201_50).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                dropZoneName_262_50 = record.ToCharArray(261, 50);
                s = new String(dropZoneName_262_50).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                altitude_312_08 = record.ToCharArray(311, 8);
                s = new String(altitude_312_08).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                radius_320_05 = record.ToCharArray(319, 5);
                s = new String(radius_320_05).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                descriptive_331_100 = record.ToCharArray(330, 100);
                s = new String(descriptive_331_100).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                fssIdent_431_04 = record.ToCharArray(430, 4);
                s = new String(fssIdent_431_04).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write('~');

                pjaUse_465_08 = record.ToCharArray(464, 8);
                s = new String(pjaUse_465_08).Trim();
                ofilePJA1.Write(s);
                ofilePJA1.Write(ofilePJA1.NewLine);
            }

            r = String.Compare(rt, "PJA2");
            if (r == 0)
            {
                pjaId_005_06 = record.ToCharArray(4, 6);
                String s = new String(pjaId_005_06).Trim();
                ofilePJA2.Write(s);
                ofilePJA2.Write('~');

                times_011_75 = record.ToCharArray(10, 75);
                s = new String(times_011_75).Trim();
                ofilePJA2.Write(s);
                ofilePJA2.Write(ofilePJA2.NewLine);
            }

            r = String.Compare(rt, "PJA3");
            if (r == 0)
            {
                pjaId_005_06 = record.ToCharArray(4, 6);
                String s = new String(pjaId_005_06).Trim();
                ofilePJA3.Write(s);
                ofilePJA3.Write('~');

                userGroupName_011_75 = record.ToCharArray(10, 75);
                s = new String(userGroupName_011_75).Trim();
                ofilePJA3.Write(s);
                ofilePJA3.Write('~');

                description_086_75 = record.ToCharArray(85, 75);
                s = new String(description_086_75).Trim();
                ofilePJA3.Write(s);
                ofilePJA3.Write(ofilePJA3.NewLine);
            }

            r = String.Compare(rt, "PJA4");
            if (r == 0)
            {
                pjaId_005_06 = record.ToCharArray(4, 6);
                String s = new String(pjaId_005_06).Trim();
                ofilePJA4.Write(s);
                ofilePJA4.Write('~');

                contactFacilityId_011_04 = record.ToCharArray(10, 4);
                s = new String(contactFacilityId_011_04).Trim();
                ofilePJA4.Write(s);
                ofilePJA4.Write('~');

                commercialFrequency_067_08 = record.ToCharArray(66, 8);
                s = new String(commercialFrequency_067_08).Trim();
                ofilePJA4.Write(s);
                ofilePJA4.Write('~');

                militaryFrequency_076_08 = record.ToCharArray(75, 8);
                s = new String(militaryFrequency_076_08).Trim();
                ofilePJA4.Write(s);
                ofilePJA4.Write(ofilePJA4.NewLine);
            }

            r = String.Compare(rt, "PJA5");
            if (r == 0)
            {
                pjaId_005_06 = record.ToCharArray(4, 6);
                String s = new String(pjaId_005_06).Trim();
                ofilePJA5.Write(s);
                ofilePJA5.Write('~');

                remark_011_300 = record.ToCharArray(10, 300);
                StringBuilder sb = new StringBuilder();
                foreach (Char c in remark_011_300)
                {
                    if ((c > 31) && (c < 97))
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }
                s = sb.ToString().Trim();
                ofilePJA5.Write(s);
                ofilePJA5.Write(ofilePJA5.NewLine);
            }

        }

    }
}
