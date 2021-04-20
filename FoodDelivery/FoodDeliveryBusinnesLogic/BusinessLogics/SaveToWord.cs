using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FoodDeliveryBusinnesLogic.HelperModels;
using System.Collections.Generic;

namespace FoodDeliveryBusinnesLogic.BusinessLogics
{
    static class SaveToWord
    {
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument
                .Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {
                        (info.Title, new WordTextProperties
                        {
                            Bold = true,
                            Size = "24",
                        })},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                foreach (var set in info.Sets)
                {
                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> {
                            (set.SetName + ": ",
                            new WordTextProperties
                            {
                                Bold = true,
                                Size = "24",
                            }),
                            (set.Price.ToString(),
                            new WordTextProperties
                            {
                                Bold = false,
                                Size = "24",
                            })
                        },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationValues = JustificationValues.Both
                        }
                    }));
                }
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }

        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Item1,
                        Space = SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }

        private static ParagraphProperties CreateParagraphProperties(WordTextProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val = paragraphProperties.Size
                    });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }

        public static void CreateStoreDoc(WordStoreInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument
                .Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {
                        (info.Title, new WordTextProperties
                        {
                            Bold = true,
                            Size = "24",
                        })},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                Table table = new Table();
                table.AppendChild(CreateTableProperties());
                string firstColumnWidth = "3000";
                string secondColumnWidth = "3000";
                string thirdColumnWidth = "2000";
                table.Append(CreateTableRow(new List<(string, string)>
                    {
                        ("Название",firstColumnWidth),
                        ("Ответственный",secondColumnWidth),
                        ("Дата создания",thirdColumnWidth)
                    }));

                foreach (var store in info.Stores)
                {
                    table.Append(CreateTableRow(new List<(string, string)>
                    {
                        (store.StoreName,firstColumnWidth),
                        (store.FullNameResponsible,secondColumnWidth),
                        (store.CreationDate.ToString(),thirdColumnWidth)
                    }));
                }
                docBody.AppendChild(table);
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        private static TableProperties CreateTableProperties()
        {
            TableProperties tableProperties = new TableProperties();
            TableBorders tableBorders = new TableBorders(
                        new TopBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 10
                        },
                        new BottomBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 10
                        },
                        new LeftBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 10
                        },
                        new RightBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 10
                        },
                        new InsideHorizontalBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 8
                        },
                        new InsideVerticalBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 8
                        }
                    );
            tableProperties.AppendChild(tableBorders);
            TableJustification tableJustification = new TableJustification() { Val = TableRowAlignmentValues.Center };
            tableProperties.AppendChild(tableJustification);
            return tableProperties;
        }

        private static TableRow CreateTableRow(List<(string, string)> columns)
        {
            TableRow tableRow = new TableRow();
            foreach (var column in columns)
            {
                TableCell cell = new TableCell();
                cell.Append(new TableCellProperties(
                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = column.Item2 }));
                cell.Append(new Paragraph(new Run(new Text(column.Item1))));
                tableRow.Append(cell);
            }
            return tableRow;
        }
    }
}
