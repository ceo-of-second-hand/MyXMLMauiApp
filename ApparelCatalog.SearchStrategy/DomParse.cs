using System.Xml;
using ApparelCatalog;
namespace ApparelCatalog.SearchStrategy
{
    public class Dom : IStrategy
    {
        private string xmlFilePath;

        public Dom(string filePath)
        {
            xmlFilePath = filePath;
        }

        public List<ClothingPiece> Search(ClothingPiece clothingPiece)
        {
            List<ClothingPiece> results = new List<ClothingPiece>();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath); // Використання шляху, переданого через конструктор

                foreach (XmlNode node in doc.DocumentElement.SelectNodes("ClothingPiece"))
                {
                    string brand = node.Attributes["Brand"]?.Value;
                    string releaseYear = node.Attributes["ReleaseYear"]?.Value;
                    string colorScheme = node.Attributes["ColorScheme"]?.Value;
                    string typeOfPiece = node.Attributes["TypeOfPiece"]?.Value;

                    ClothingPiece piece = new ClothingPiece(brand, releaseYear, colorScheme, typeOfPiece);

                    if (piece.MatchesAllFilters(clothingPiece))
                    {
                        results.Add(piece);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DOM strategy: {ex.Message}");
            }

            return results;
        }
    }
}

