using System.Xml;

namespace ApparelCatalog.SearchStrategy
{
    public class Sax : IStrategy
    {
        private string xmlFilePath;

        public Sax(string filePath)
        {
            xmlFilePath = filePath;
        }

        public List<ClothingPiece> Search(ClothingPiece clothingPiece)
        {
            List<ClothingPiece> results = new List<ClothingPiece>();

            try
            {
                using (XmlTextReader xmlReader = new XmlTextReader(xmlFilePath)) // Використання шляху, переданого через конструктор
                {
                    while (xmlReader.Read())
                    {
                        // Перевірка: чи є вузол xml елементом, чи це "ClothingPiece", чи має атрибути
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "ClothingPiece" && xmlReader.HasAttributes)
                        {
                            string brand = xmlReader.GetAttribute("Brand");
                            string releaseYear = xmlReader.GetAttribute("ReleaseYear");
                            string colorScheme = xmlReader.GetAttribute("ColorScheme");
                            string typeOfPiece = xmlReader.GetAttribute("TypeOfPiece");

                            ClothingPiece piece = new ClothingPiece(brand, releaseYear, colorScheme, typeOfPiece);

                            if (piece.MatchesAllFilters(clothingPiece))
                            {
                                results.Add(piece);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SAX strategy: {ex.Message}");
            }

            return results;
        }
    }
}
