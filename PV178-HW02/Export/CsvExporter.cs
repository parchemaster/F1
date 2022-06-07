using System.Collections;
using System.Runtime.InteropServices;

namespace PV178_HW02.Export
{
    internal class CsvExporter
    {
        public static void Export(string fileName, ICollection collection) // you can update the second argument type based on your collection
        {
            string path;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, fileName); // update the path if necessary (Windows)
            }
            else
            {
                path = Path.Combine(Directory.GetCurrentDirectory()
                    .Replace("bin/Debug/net6.0", "Export"), fileName); // didn't find perfect function for Mac OS
            }

            using FileStream fs = File.Create(path);
            using var sr = new StreamWriter(fs);

            sr.WriteLine("id;activity");

            foreach (String record in collection)
            {
                String[] words = record.Split(";");
                // update this based on your collection
                var caseID = words[0]; 
                // update this based on your collection
                var activity = words[1];
                sr.WriteLine($"{caseID};{activity}");
            }
        }
    }
}
