using Data.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;

namespace Data.Report
{
    public class Report
    {
        public List<Car> ListCars { get; set; }
        
        public void CreateReport(string fileName)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create($@"C:\Users\Volobuev\Desktop\DbToXML\DbToXML\App_Data\{fileName}.xlsx", SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                
                var autoFilter = new AutoFilter { Reference = "A1:C1" };
                
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();
                
                ///////////////////////////////
                var sheetViews1 = new SheetViews();
                var sheetView1 = new SheetView { WorkbookViewId = 0U };
                var cellNumber = GetCellNumber(3, 20);
                var selection1 = new Selection { ActiveCell = cellNumber, SequenceOfReferences = new ListValue<StringValue>() { InnerText = cellNumber } };

                sheetView1.Append(selection1);
                sheetViews1.Append(sheetView1);
                worksheetPart.Worksheet.Append(sheetViews1);
                ///////////////////////////////
                
                var stylePart = workbookPart.AddNewPart<WorkbookStylesPart>(); // Adding style
                stylePart.Stylesheet = GenerateStylesheet();
                
                var columns = new Columns(new Column { Min = 1, Max = 3, Width = 25, CustomWidth = true });  // Setting up columns
                worksheetPart.Worksheet.AppendChild(columns);

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Cars" };
                sheets.Append(sheet);
                
                Data(worksheetPart); // add cars 

                workbookPart.Workbook.Save();
                worksheetPart.Worksheet.Append(autoFilter);
                worksheetPart.Worksheet.Save();
            }
        }

        private void Data(WorksheetPart worksheetPart)
        {
            var sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
            var row = new Row();

            row.Append(ConstructCell("Марка", CellValues.String, 2), ConstructCell("Модель", CellValues.String, 2), ConstructCell("Год выпуска", CellValues.String, 2));
            sheetData.AppendChild(row);

            foreach (var car in ListCars)
            {
                row = new Row();
                row.Append(ConstructCell(car.Model.ToString(), CellValues.String, 1), ConstructCell(car.Name.ToString(), CellValues.String, 1), ConstructCell(car.Year.Date.ToString(), CellValues.Number, 1));
                sheetData.AppendChild(row);
            }
        }

        private Cell ConstructCell(string value, CellValues dataType, uint styleIndex = 0)
        {
            return new Cell() { CellValue = new CellValue(value), DataType = new EnumValue<CellValues>(dataType), StyleIndex = styleIndex };
        }
        
        private Stylesheet GenerateStylesheet()
        {
            var fonts = new Fonts(
                new Font(new FontSize { Val = 11 }),                                            // Index 0 - default
                new Font(new FontSize { Val = 11 }, new Bold(), new Color { Rgb = "FFFFFF" })); // Index 1 - header

            var fills = new Fills(
                    new Fill(new PatternFill { PatternType = PatternValues.None }),                                                                             // Index 0 - default
                    new Fill(new PatternFill { PatternType = PatternValues.Gray125 }),                                                                          // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "66666666" } }) { PatternType = PatternValues.Solid })  // Index 2 - header
                );

            var borders = new Borders(
                    new Border(), // index 0 default
                    new Border(new LeftBorder(new Color { Auto = true }) { Style = BorderStyleValues.Thin },
                               new RightBorder(new Color { Auto = true }) { Style = BorderStyleValues.Thin },
                               new TopBorder(new Color { Auto = true }) { Style = BorderStyleValues.Thin },
                               new BottomBorder(new Color { Auto = true }) { Style = BorderStyleValues.Thin })); // index 1 black border

            var cellFormats = new CellFormats(
                    new CellFormat(),                                                                           // default
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true },                // body
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFill = true, ApplyFont = true } // header
                );

            return new Stylesheet(fonts, fills, borders, cellFormats);
        }

        private string GetCellNumber(int horizontal, int vertical)
        {
            var mass = new List<int>();

            while (horizontal > 26)
            {
                if (horizontal % 26 != 0)
                {
                    mass.Add((horizontal % 26) + 64);

                }
                else
                {
                    while (horizontal > 26)
                    {
                        if (horizontal % 26 != 0)
                        {
                            mass.Add((horizontal % 26 + 24) + 65);

                        }
                        horizontal = horizontal / 26;
                    }
                    mass.Add((horizontal + 24) + 65);

                }
                horizontal = horizontal / 26;
            }
            if (horizontal != 0)
            {
                mass.Add(horizontal + 64);
            }
            mass.Reverse();

            var result = "";
            foreach (int i in mass)
            {
                result += Convert.ToString((char)(i));
            }

            return (result + vertical);
        }
    }
}
