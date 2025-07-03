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

This package includes the .NET and .NET Core assemblies required to export report to WYSIWYG PDF format.

It provides different features, like export to different PDF versions, support of encryption, support for ZUGFeRD attachments and metadata, good accessibility, AcroForms (professional license required), and others.


## Note

AcroForms are not supported for Section reports with [GDI](https://developer.mescius.com/activereportsnet/docs/latest/online/GrapeCity.ActiveReports~GrapeCity.ActiveReports.Document.CompatibilityModes.html) mode.

## License

Some PDF-related features are not available with a Standard Edition license:
* Exporting with bold font emulation. It is required to obtain WYSIWYG output in case of font fallback (if cannot find an appropriate bold font).
* Exporting with [IVS](https://en.wikipedia.org/wiki/Variant_form_(Unicode)) character glyphs.
* Exporting with MS Windows [EUDC](https://docs.microsoft.com/en-us/windows/win32/intl/end-user-defined-characters) glyphs.
* Exporting with InputFields/AcroForms (a mechanism to add editable forms to the PDF file format).
* Exporting with predefined printing properties: page scaling, duplex mode, paper source by page size, print page range, number of copies.
* Signing with [Digital signature](https://en.wikipedia.org/wiki/Digital_signature) certificates.
* Adding a document [Timestamp](https://en.wikipedia.org/wiki/Timestamp).
* Exporting to [PDF/A](https://en.wikipedia.org/wiki/PDF/A) for archival preservation.
* Making the documents compliant with "Section 508" and [PDF/UA](https://en.wikipedia.org/wiki/PDF/UA).

# See also

* [MESCIUS.ActiveReports.Viewer.Win](https://www.nuget.org/packages/MESCIUS.ActiveReports.Viewer.Win/)
* [MESCIUS.ActiveReports.Viewer.Wpf](https://www.nuget.org/packages/MESCIUS.ActiveReports.Viewer.Wpf/)
* [MESCIUS.ActiveReports.Web](https://www.nuget.org/packages/MESCIUS.ActiveReports.Web/)
* [MESCIUS.ActiveReports.Aspnetcore.Viewer](https://www.nuget.org/packages/MESCIUS.ActiveReports.Aspnetcore.Viewer/)
* [MESCIUS.ActiveReports.Blazor.Viewer](https://www.nuget.org/packages/MESCIUS.ActiveReports.Blazor.Viewer/)
* [MESCIUS.ActiveReports.Design.Win](https://www.nuget.org/packages/MESCIUS.ActiveReports.Design.Win/)
* [MESCIUS.ActiveReports.Aspnetcore.Designer](https://www.nuget.org/packages/MESCIUS.ActiveReports.Aspnetcore.Designer/)
* [MESCIUS.ActiveReports.Export.Pdf](https://www.nuget.org/packages/MESCIUS.ActiveReports.Export.Pdf/)