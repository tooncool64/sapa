using SAPA.Components.PDF.Model;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;

namespace SAPA.Components.PDF.Templates{
    public class AppealDocument : IDocument
    {
        public AppealModel Model { get; }

        public AppealDocument(AppealModel model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(50);

                page.Header().Text("Student Appeal Document")
                    .FontSize(20).SemiBold().AlignCenter();

                page.Content().Element(ComposeContent);

                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(20);

                column.Item().Text($"Name: {Model.StudentName}").FontSize(14);
                column.Item().Text($"Student ID: {Model.StudentID}").FontSize(14);
                column.Item().Text($"Reason for Appeal:").FontSize(14).SemiBold();
                column.Item().Text(Model.AppealReason).FontSize(12);

                column.Item().Text($"Requested Action:").FontSize(14).SemiBold();
                column.Item().Text(Model.RequestedAction).FontSize(12);

                if (!string.IsNullOrWhiteSpace(Model.Comments))
                {
                    column.Item().Text($"Additional Comments:").FontSize(14).SemiBold();
                    column.Item().Text(Model.Comments).FontSize(12);
                }
            });
        }
    }
}