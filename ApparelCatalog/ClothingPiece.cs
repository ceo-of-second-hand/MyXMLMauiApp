namespace ApparelCatalog
{
    public class ClothingPiece
    {
        public ClothingPiece()
        {
        }

        public ClothingPiece(string brand, string releaseYear, string colorScheme, string typeOfPiece)
        {
            Brand = brand;
            ReleaseYear = releaseYear;
            ColorScheme = colorScheme;
            TypeOfPiece = typeOfPiece;
        }

        public string Brand { get; set; } = "";
        public string ReleaseYear { get; set; } = "";
        public string ColorScheme { get; set; } = "";
        public string TypeOfPiece { get; set; } = "";

        public bool MatchesAllFilters(ClothingPiece filter)
        {
            return (string.IsNullOrEmpty(filter.Brand) || string.Equals(Brand, filter.Brand, StringComparison.OrdinalIgnoreCase)) &&
                   (string.IsNullOrEmpty(filter.ReleaseYear) || string.Equals(ReleaseYear, filter.ReleaseYear, StringComparison.OrdinalIgnoreCase)) &&
                   (string.IsNullOrEmpty(filter.ColorScheme) || string.Equals(ColorScheme, filter.ColorScheme, StringComparison.OrdinalIgnoreCase)) &&
                   (string.IsNullOrEmpty(filter.TypeOfPiece) || string.Equals(TypeOfPiece, filter.TypeOfPiece, StringComparison.OrdinalIgnoreCase));
        }

    }
}
