using iTextSharp.text;
using iTextSharp.text.pdf;

public class CustomPdfPageEventHelper : PdfPageEventHelper
{
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        PdfContentByte contentByte = writer.DirectContent;
        Rectangle pageSize = document.PageSize;

        contentByte.BeginText();
        contentByte.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10);

        float x = pageSize.GetRight(70);
        float y = pageSize.GetBottom(30);

        contentByte.SetTextMatrix(x, y);

        contentByte.ShowText("Pagina " + writer.PageNumber.ToString());


        contentByte.EndText();
    }
}
