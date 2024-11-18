using System.Xml;

namespace ApparelCatalog.SearchStrategy;

public interface IStrategy
{
    List<ClothingPiece> Search(ClothingPiece clothing_piece);
}
