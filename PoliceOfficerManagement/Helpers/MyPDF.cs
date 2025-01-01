using DinkToPdf;
using DinkToPdf.Contracts;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PoliceOfficerManagement.Helpers
{
    public class MyPDF
    {
        private readonly string rootPath;
        private readonly IConverter _converter;

        public MyPDF(IWebHostEnvironment hostingEnvironment, IConverter converter)
        {
            this.rootPath = hostingEnvironment.ContentRootPath + "/wwwroot/pdf/";
            _converter = converter;
        }
        //[STAThread]
        public string GeneratePDF(out string fileName, string url)
        {
            string status = "done";
            fileName = "Document_" + DateTime.Now.ToString("yyyy-MM-dd_HH-ss") + ".pdf";
            var prinrDate = DateTime.Now;
            var footerLeft = $"        Print Date: {prinrDate:dd-MMM-yyyy} Time: {prinrDate:h:mm tt}";
            var footerRight = "Computer Generated Report does not require any signature        ";
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 12, Bottom = 9, Left = 14, Right = 14 },
                    Out = rootPath+fileName
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HeaderSettings = { FontName = "Arial", FontSize = 3, Right = "", Line = false, HtmUrl=""},
                        FooterSettings = { FontName = "Nikosh", FontSize = 8, Line = false, Center = "", 
                            Left= footerLeft,
                           Right = footerRight
                        },
                        Page =url,
                    }
                }
            };

            try
            {
                _converter.Convert(doc);
            }
            catch (Exception e)
            {
                status = e.Message;
            }

            return status;
        }

        public string GeneratePDFQr(out string fileName, string url)
        {
            string status = "done";
            fileName = "Document_" + DateTime.Now.ToString("yyyy-MM-dd_HH-ss") + ".pdf";
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 12, Bottom = 9, Left = 14, Right = 14 },
                    Out = rootPath+fileName
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HeaderSettings = { FontName = "Arial", FontSize = 3, Right = "", Line = false, HtmUrl=""},
                        FooterSettings = { FontName = "Nikosh", FontSize = 8, Line = false, Center = "",                           
                        },
                        Page =url,
                    }
                }
            };

            try
            {
                _converter.Convert(doc);
            }
            catch (Exception e)
            {
                status = e.Message;
            }

            return status;
        }
        public string GeneratePDFForPaymentReceipt(out string fileName, string url)
        {
            string status = "done";
            fileName = "Document_" + DateTime.Now.ToString("yyyy-MM-dd_HH-ss") + ".pdf";
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 12, Bottom = 9, Left = 14, Right = 14 },
                    Out = rootPath+fileName
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HeaderSettings = { FontName = "Arial", FontSize = 3, Right = "", Line = false, HtmUrl=""},
                        FooterSettings = { FontName = "Nikosh", FontSize = 8, Line = false, Center = ""
                        },
                        Page =url,
                    }
                }
            };

            try
            {
                _converter.Convert(doc);
            }
            catch (Exception e)
            {
                status = e.Message;
            }

            return status;
        }



        public string GeneratePDFLegal(out string fileName, string url)
        {
            string status = "done";
            fileName = "Document_" + DateTime.Now.ToString("yyyy-MM-dd_HH-ss") + ".pdf";

            var prinrDate = DateTime.Now;
            var footerLeft = $"        Print Date: {prinrDate:dd-MMM-yyyy} Time: {prinrDate:h:mm tt}";
            var footerRight = "Computer Generated Report does not require any signature        ";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.Legal,
                    Margins = new MarginSettings() { Top = 12, Bottom = 9, Left = 14, Right = 14 },
                    Out = rootPath+fileName
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HeaderSettings = { FontName = "Arial", FontSize = 3, Right = "", Line = false, HtmUrl=""},
                        FooterSettings = { FontName = "Nikosh", FontSize = 8, Line = false, Center = " ", Left= footerLeft,
                           Right = footerRight },
                        Page =url,
                    }
                }
            };

            try
            {
                _converter.Convert(doc);
            }
            catch (Exception e)
            {
                status = e.Message;
            }

            return status;
        }
        //[STAThread]
        public string GenerateLandscapePDF(out string fileName, string url)
        {
            string status = "done";
            fileName = "Document_" + DateTime.Now.ToString("yyyy-MM-dd_HH-ss") + ".pdf";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 12, Bottom = 9, Left = 14, Right = 14 },
                    Out = rootPath+fileName
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HeaderSettings = { FontName = "Arial", FontSize = 3, Right = "", Line = false, HtmUrl=""},
                        FooterSettings = { FontName = "Arial", FontSize = 6, Line = true, Center = " " },
                        Page =url,
                    }
                }
            };

            try
            {
                _converter.Convert(doc);
            }
            catch (Exception e)
            {
                status = e.Message;
            }

            return status;
        }

        public string GeneratePOSReceiptPDF(out string fileName, string url, int item, string invoiceNo)
        {
            string status = "done";
            fileName = "Document_POS_" + invoiceNo + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-ss") + ".pdf";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = new PechkinPaperSize("65mm",item+"mm"),

                    Margins = new MarginSettings() { Top = 2, Bottom = 0, Left = .5, Right =0 },
                    Out = rootPath+fileName
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HeaderSettings = { FontName = "Arial", FontSize = 3, Right = "", Line = false, HtmUrl=""},
                        FooterSettings = { FontName = "Arial", FontSize = 6, Line = true, Center = " " },
                        Page =url,
                    }
                }
            };

            try
            {
                _converter.Convert(doc);
            }
            catch (Exception e)
            {
                status = e.Message;
            }

            return status;
        }
        public string MergedPOSReceiptPDF(out string outputFilePath, string fileName, string fileName1, string invoiceNo)
        {
            outputFilePath = rootPath + "Document_POS_" + invoiceNo + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-ss") + ".pdf";

            try
            {

                using (var stream1 = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    // Create a Document instance for the output file
                    using (var document = new Document())
                    {
                        // Create a PdfCopy instance to handle the merging
                        using (var copy = new PdfCopy(document, stream1))
                        {
                            document.Open();
                            using (var reader = new PdfReader(rootPath + fileName))
                            {
                                // Copy pages from the input PDF to the output PDF
                                copy.AddDocument(reader);
                            }
                            using (var reader = new PdfReader(rootPath + fileName1))
                            {
                                // Copy pages from the input PDF to the output PDF
                                copy.AddDocument(reader);
                            }
                            document.Close();
                        }
                    }
                }
                return "Merged";

            }
            catch (Exception ex)
            {
                return "NotMerged";
            }
        }
    }
}
