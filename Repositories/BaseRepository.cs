using System.Collections.Generic;
using System.IO;

namespace SolarManagement
{
    // BASE CLASS for every repository. It holds the shared file code,
    // so the other repositories INHERIT it instead of repeating it.
    public class BaseRepository
    {
        public string FilePath;

        public BaseRepository(string filePath)
        {
            FilePath = filePath;
        }

        // Reads every non-empty line of the file into a list.
        public List<string> ReadLines()
        {
            List<string> lines = new List<string>();

            if (!File.Exists(FilePath))
            {
                return lines;
            }

            using (StreamReader reader = new StreamReader(FilePath))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line != "")
                    {
                        lines.Add(line);
                    }
                    line = reader.ReadLine();
                }
            }
            return lines;
        }

        // Adds one line at the end of the file.
        public void AppendLine(string line)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine(line);
            }
        }

        // Replaces the whole file with the given lines.
        public void WriteLines(List<string> lines)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, false))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    writer.WriteLine(lines[i]);
                }
            }
        }
    }
}
