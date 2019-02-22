using System.IO;
using PolicyMaturity.Contracts;

namespace PolicyMaturity.BusinessLayer
{
    public class CsvValidator : IValidator
    {
        private readonly string fileExtension = ".csv";

        public void Validate(string path)
        {
            string extension = Path.GetExtension(path);

            if (!File.Exists(path) || extension != fileExtension)
               throw new FileNotFoundException("The file was not found", path);
        }
    }
}
