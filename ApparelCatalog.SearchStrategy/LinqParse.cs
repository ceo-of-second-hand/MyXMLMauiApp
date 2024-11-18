using System.Xml.Linq;

namespace ApparelCatalog.SearchStrategy
{
    public class Linq : IStrategy
    {
        private string xmlFilePath;

        public Linq(string filePath)
        {
            xmlFilePath = filePath;
        }

        public List<ClothingPiece> Search(ClothingPiece clothingPiece)
        {
            List<ClothingPiece> results = new List<ClothingPiece>();

            try
            {
                var doc = XDocument.Load(xmlFilePath); // Використання шляху, переданого через конструктор

                // Знаходимо всі вузли <ClothingPiece>
                var result = doc.Descendants("ClothingPiece")
                                .Select(obj => new ClothingPiece(
                                    (string)obj.Attribute("Brand"),
                                    (string)obj.Attribute("ReleaseYear"),
                                    (string)obj.Attribute("ColorScheme"),
                                    (string)obj.Attribute("TypeOfPiece")))
                                .Where(piece => piece.MatchesAllFilters(clothingPiece));

                results.AddRange(result); // Додаємо всі знайдені елементи до списку
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LINQ strategy: {ex.Message}");
            }

            return results;
        }
    }
}
