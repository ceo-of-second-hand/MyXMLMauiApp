using System.Xml.Xsl;
using ApparelCatalog;
using ApparelCatalog.SearchStrategy;
using System.Xml;
using System.IO;

namespace MyXMLMauiApp
{
    public partial class MainPage : ContentPage
    {
        // Define file paths relative to the project's directory
        private static readonly string BasePath = Path.Combine(AppContext.BaseDirectory, "Data");
        private static readonly string XmlFilePath = Path.Combine(BasePath, "xml2.xml");
        private static readonly string XslFilePath = Path.Combine(BasePath, "xsl2.xsl");
        private static readonly string HtmlFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ClothingCatalog.html");

        public MainPage()
        {
            InitializeComponent();
            PopulatePickers();
        }

        private void PopulatePickers()
        {
            IStrategy analyzer = new Dom(XmlFilePath); // Using DOM strategy to populate pickers
            var allPieces = analyzer.Search(new ClothingPiece());

            foreach (var piece in allPieces)
            {
                if (!string.IsNullOrWhiteSpace(piece.Brand) && !BrandPicker.Items.Contains(piece.Brand))
                    BrandPicker.Items.Add(piece.Brand);

                if (!string.IsNullOrWhiteSpace(piece.ReleaseYear) && !ReleaseYearPicker.Items.Contains(piece.ReleaseYear))
                    ReleaseYearPicker.Items.Add(piece.ReleaseYear);

                if (!string.IsNullOrWhiteSpace(piece.ColorScheme) && !ColorSchemePicker.Items.Contains(piece.ColorScheme))
                    ColorSchemePicker.Items.Add(piece.ColorScheme);

                if (!string.IsNullOrWhiteSpace(piece.TypeOfPiece) && !TypeOfPiecePicker.Items.Contains(piece.TypeOfPiece))
                    TypeOfPiecePicker.Items.Add(piece.TypeOfPiece);
            }
        }

        private void SearchButtonClicked(object sender, EventArgs e)
        {
            editor.Text = "";

            var clothingPiece = SelectCategories();
            var analyzer = SelectAnalyzer();
            var results = analyzer.Search(clothingPiece);

            foreach (var piece in results)
            {
                editor.Text += $"{piece.Brand}\n";
                editor.Text += $"{piece.ReleaseYear}\n";
                editor.Text += $"{piece.ColorScheme}\n";
                editor.Text += $"{piece.TypeOfPiece}\n";
                editor.Text += $"-----------------------\n";
            }
        }

        private ClothingPiece SelectCategories()
        {
            return new ClothingPiece
            {
                Brand = BrandCheckBox.IsChecked ? BrandPicker.SelectedItem?.ToString() : null,
                ReleaseYear = ReleaseYearCheckBox.IsChecked ? ReleaseYearPicker.SelectedItem?.ToString() : null,
                ColorScheme = ColorSchemeCheckBox.IsChecked ? ColorSchemePicker.SelectedItem?.ToString() : null,
                TypeOfPiece = TypeOfPieceCheckBox.IsChecked ? TypeOfPiecePicker.SelectedItem?.ToString() : null
            };
        }

        private IStrategy SelectAnalyzer()
        {
            if (DomRadioButton.IsChecked) return new Dom(XmlFilePath);
            if (LinqRadioButton.IsChecked) return new Linq(XmlFilePath);
            return new Sax(XmlFilePath);
        }

        private void ClearButtonClicked(object sender, EventArgs e)
        {
            editor.Text = "";

            // Reset all checkboxes
            foreach (CheckBox checkBox in new[] { BrandCheckBox, ReleaseYearCheckBox, ColorSchemeCheckBox, TypeOfPieceCheckBox })
            {
                checkBox.IsChecked = false;
            }

            // Reset all radio buttons
            foreach (RadioButton radioButton in new[] { SaxRadioButton, DomRadioButton, LinqRadioButton })
            {
                radioButton.IsChecked = false;
            }

            // Reset all pickers
            foreach (var picker in new[] { BrandPicker, ReleaseYearPicker, ColorSchemePicker, TypeOfPiecePicker })
            {
                picker.SelectedItem = null;
            }
        }

        private void TransformToHTMLButtonClicked(object sender, EventArgs e)
        {
            var xct = LoadXSLT();

            TransformXMLToHTML(xct, CreateXSLTArguments(), XmlFilePath, HtmlFilePath);

            // Notify the user that the HTML file has been generated
            editor.Text = $"HTML file generated on Desktop:\n{HtmlFilePath}";
        }

        private XslCompiledTransform LoadXSLT()
        {
            var xct = new XslCompiledTransform();
            xct.Load(XslFilePath);
            return xct;
        }

        private XsltArgumentList CreateXSLTArguments()
        {
            var args = new XsltArgumentList();
            if (BrandCheckBox.IsChecked) args.AddParam("brand", "", BrandPicker.SelectedItem?.ToString());
            if (ReleaseYearCheckBox.IsChecked) args.AddParam("releaseYear", "", ReleaseYearPicker.SelectedItem?.ToString());
            if (ColorSchemeCheckBox.IsChecked) args.AddParam("colorScheme", "", ColorSchemePicker.SelectedItem?.ToString());
            if (TypeOfPieceCheckBox.IsChecked) args.AddParam("typeOfPiece", "", TypeOfPiecePicker.SelectedItem?.ToString());
            return args;
        }

        private void TransformXMLToHTML(XslCompiledTransform xct, XsltArgumentList args, string xmlPath, string htmlPath)
        {
            using var xr = XmlReader.Create(xmlPath);
            using var xw = XmlWriter.Create(htmlPath);
            xct.Transform(xr, args, xw);
        }

        private async void ExitButtonClicked(object sender, EventArgs e)
        {
            var result = await Application.Current.MainPage.DisplayAlert("Exit", "Do you really want to exit the application?", "Yes", "No");
            if (result)
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
        }
    }
}
