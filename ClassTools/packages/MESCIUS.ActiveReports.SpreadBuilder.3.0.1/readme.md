# Introduction

 ActiveReports is a unique collection of developer reporting tools that help consume, process, and visualize data in the form of compelling and easy-to-understand reports.

 ActiveReports provides a lot of features for developers and end-users, like:
* VisualStudio integration support.
* PaaS support (like Azure Linux App Service).
* Different possibilities to pivot and aggregate data.
* Rich data visualization.
* Popular export formats (like PDF/Excel/Word).
* And a lot more (https://developer.mescius.com/activereportsnet).

# Concept

This package includes the .NET and .NET Core assemblies for reading, manipulating and writing Excel files. So you can create Excel spreadsheets cell by cell.

**Example:**
```c#
// Create a Workbook and add a sheet to its Sheets collection
System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(1, 1);
System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
GrapeCity.SpreadBuilder.Workbook sb = new GrapeCity.SpreadBuilder.Workbook((text, measurementParams) =>
{
    // required for auto-row height function only
    System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
    if (measurementParams.FontBold) style |= System.Drawing.FontStyle.Bold;
    if (measurementParams.FontItalic) style |= System.Drawing.FontStyle.Italic;
    using (System.Drawing.Font gFont = new System.Drawing.Font(measurementParams.FontName, measurementParams.FontSize, style))
    {
        int maxWidth = (int)System.Math.Ceiling(measurementParams.MaxWidth);
        System.Drawing.SizeF size = graphics.MeasureString(text, gFont, maxWidth == 0 ? 999999 : maxWidth);
        return new System.Drawing.SizeF((float)System.Math.Ceiling(size.Width), (float)System.Math.Ceiling(size.Height));
    }
});
sb.Sheets.AddNew();
  
// Set up properties and values for columns, rows and cells as desired
sb.Sheets[0].Name = "Customer Call List";
sb.Sheets[0].Columns(0).Width = 2 * 1440;
sb.Sheets[0].Columns(1).Width = 1440;
sb.Sheets[0].Columns(2).Width = 1440;
sb.Sheets[0].Rows(0).Height = 1440 / 4;
  
// Header row
sb.Sheets[0].Cell(0,0).SetValue("Company Name");
sb.Sheets[0].Cell(0,0).FontBold = true;
sb.Sheets[0].Cell(0,1).SetValue("Contact Name");
sb.Sheets[0].Cell(0,1).FontBold = true;
sb.Sheets[0].Cell(0,2).SetValue("Phone");
sb.Sheets[0].Cell(0,2).FontBold = true;
  
// First row of data
sb.Sheets[0].Cell(1,0).SetValue("GrapeCity");
sb.Sheets[0].Cell(1,1).SetValue("Mortimer");
sb.Sheets[0].Cell(1,2).SetValue("(425) 880-2601");
  
// Save the Workbook to an Excel file
sb.Save("test.xls");
```
# See also
* [MESCIUS.ActiveReports.Viewer.Win](https://www.nuget.org/packages/MESCIUS.ActiveReports.Viewer.Win/)
* [MESCIUS.ActiveReports.Viewer.Wpf](https://www.nuget.org/packages/MESCIUS.ActiveReports.Viewer.Wpf/)
* [MESCIUS.ActiveReports.Web](https://www.nuget.org/packages/MESCIUS.ActiveReports.Web/)
* [MESCIUS.ActiveReports.Aspnetcore.Viewer](https://www.nuget.org/packages/MESCIUS.ActiveReports.Aspnetcore.Viewer/)
* [MESCIUS.ActiveReports.Blazor.Viewer](https://www.nuget.org/packages/MESCIUS.ActiveReports.Blazor.Viewer/)
* [MESCIUS.ActiveReports.Design.Win](https://www.nuget.org/packages/MESCIUS.ActiveReports.Design.Win/)
* [MESCIUS.ActiveReports.Aspnetcore.Designer](https://www.nuget.org/packages/MESCIUS.ActiveReports.Aspnetcore.Designer/)
* [MESCIUS.ActiveReports.Export.Pdf](https://www.nuget.org/packages/MESCIUS.ActiveReports.Export.Pdf/)