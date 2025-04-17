

using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

Console.WriteLine("Hello ");
SplitPdf("/workspaces/PdfReadAndConvertJson/112TEMEL_A.pdf");

static void SplitPdf(string src)
{
    PdfDocument pdfDoc = new PdfDocument(new PdfReader(src));

    for(int i =1; i<pdfDoc.GetNumberOfPages(); i++)
    {
        var originalPage = pdfDoc.GetPage(1);
        var mediaBox = originalPage.GetPageSize();
        float width = mediaBox.GetWidth();
        float height = mediaBox.GetHeight();
        // Sol yarı
        var leftBox = new Rectangle(0, 0, width / 2, height);
        var leftText = ExtractTextFromRegion(originalPage, leftBox);
        Console.WriteLine(leftText);
        
        // Sağ yarı
        var rightBox = new Rectangle(width / 2, 0, width / 2, height);
    }
}

static string ExtractTextFromRegion(PdfPage page, Rectangle area)
{
    var strategy = new FilteredTextEventListener(new LocationTextExtractionStrategy(),
    new TextRegionEventFilter(area));
    return PdfTextExtractor.GetTextFromPage(page, strategy);
}