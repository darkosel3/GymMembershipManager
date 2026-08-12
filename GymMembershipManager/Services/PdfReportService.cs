using GymMembershipManager.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GymMembershipManager.Services
{
    public class PdfReportService : IPdfReportService
    {
        public void GenerateReport(List<Member> members, string filePath)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);

                    page.Header().Text("Izveštaj o članovima teretane")
                        .FontSize(20).Bold().AlignCenter();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);  // Ime
                            columns.RelativeColumn(2);  // Prezime
                            columns.RelativeColumn(2);  // Telefon
                            columns.RelativeColumn(2);  // Datum učlanjenja
                            columns.RelativeColumn(1);  // Br. članarina
                        });

                        // Header row
                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Ime").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Prezime").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Telefon").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Učlanjen").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Članarine").Bold();
                        });

                        foreach (var m in members)
                        {
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5).Text(m.FirstName);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5).Text(m.LastName);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5).Text(m.PhoneNumber);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5).Text(m.DateJoined.ToString("dd.MM.yyyy"));
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5).Text(m.MemberShips.Count.ToString());
                        }
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Generisano: ");
                        text.Span(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                    });
                });
            }).GeneratePdf(filePath);
        }
    }
}