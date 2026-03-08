using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCase.Expenses.Reports.PDF.Fonts;

public class ExpenseReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);
        stream ??= ReadFontFile(FontHelper.DEFAULT_FONT);

        if (stream == null) return null;
        
        var data = new byte[stream.Length];
        stream.Read(buffer: data, offset: 0, count: (int)stream.Length);
        return data;


    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"CashFlow.Application.UseCase.Expenses.Reports.PDF.Fonts.{faceName}.ttf");
    }
}
