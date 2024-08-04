using LibraryBook.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;
using System.Drawing;

namespace LibraryBook.Web.Controllers
{
    public class TTHSController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TTHSController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string downloadType, TTHSVM info)
        {
            string basePath = _webHostEnvironment.WebRootPath;

            WordDocument document = new WordDocument();


            // Load the template.
            string dataPath = basePath + @"/exports/tths.docx";
            using FileStream fileStream = new(dataPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            document.Open(fileStream, FormatType.Automatic);

            TextSelection textSelection = document.Find("xx_class_name", false, true);
            WTextRange textRange = textSelection.GetAsOneRange();
            textRange.Text = info.ClassName;

            textSelection = document.Find("xx_student_name", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.StudentName;

            textSelection = document.Find("xx_gender", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.Gender;

            string formattedDate = info.DoB.ToString("dd/MM/yyyy");
            textSelection = document.Find("xx_dateofbirth", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = formattedDate;

            textSelection = document.Find("xx_dantoc", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.DanToc;

            textSelection = document.Find("xx_sonha", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.Sonha;

            textSelection = document.Find("xx_to", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.To;

            textSelection = document.Find("xx_ap", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.Ap;

            textSelection = document.Find("xx_xa", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.Xa;

            textSelection = document.Find("xx_quan_huyen", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.QH;

            textSelection = document.Find("xx_phone_number", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.PhoneNumber;

            textSelection = document.Find("xx_father_name", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.FatherName;

            textSelection = document.Find("xx_father_phone_number", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.FatherPhoneNumber;

            textSelection = document.Find("xx_father_job", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.FatherJob;

            textSelection = document.Find("xx_father_place_of_job", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.FatherPoJ;

            textSelection = document.Find("xx_mother_name", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.MotherName;

            textSelection = document.Find("xx_mother_phone_number", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.MotherPhoneNumber;

            textSelection = document.Find("xx_mother_job", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.MotherJob;

            textSelection = document.Find("xx_mother_place_of_job", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.MotherPoJ;

            textSelection = document.Find("xx_doan", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.Doan;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_FwM", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.FwM;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_F", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.Father;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_M", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.Mother;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_wGFM", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.GFM;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_BwS", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.BWS;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_other", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.Other;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_reason", false, true);

            if (textSelection != null)
            {
                textRange = textSelection.GetAsOneRange();
                if (string.IsNullOrEmpty(info.Reason))
                {
                    // Clear the placeholder text
                    textRange.Text = string.Empty;

                    // Insert two new rows (paragraphs) with dots
                    var paragraph = textRange.OwnerParagraph;
                    paragraph.AppendText("..........................................................................");
                    paragraph.AppendText("\r\n...................................................................................................................................................................");
                }
                else
                {
                    textRange.Text = info.Reason;
                }
            }

            textSelection = document.Find("xx_xdgn", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.XDGN;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_khokhan", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.KhoKhan;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_other2", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.Other2;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_contb", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.ConTb;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_gdcccm", false, true);
            if (textSelection != null)
            {
                var checkboxRange = textSelection.GetAsOneRange();
                checkboxRange.Text = string.Empty; // Clear the placeholder text

                // Create a checkbox content control
                WCheckBox checkbox = new WCheckBox(document);
                checkbox.Checked = info.GDCCCM;

                // Append the checkbox to the document at the placeholder location
                var paragraph = checkboxRange.OwnerParagraph;
                paragraph.ChildEntities.Insert(paragraph.ChildEntities.IndexOf(checkboxRange), checkbox);
                paragraph.ChildEntities.Remove(checkboxRange);
            }

            textSelection = document.Find("xx_ttsk", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.TTSK;

            textSelection = document.Find("xx_benhtat", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.BenhTat;

            textSelection = document.Find("xx_hanhkiem", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.HanhKiem;

            textSelection = document.Find("xx_hocluc", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.HocLuc;

            textSelection = document.Find("xx_nangkhieu", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.NangKhieu;

            textSelection = document.Find("xx_nguyenvong", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.NguyenVong;

            textSelection = document.Find("xx_hkiem2", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.HKiem2;

            textSelection = document.Find("xx_hluc2", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.HLuc2;
                        
            textSelection = document.Find("xx_ngay", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.Ngay;

            textSelection = document.Find("xx_thang", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.Thang;

            textSelection = document.Find("xx_phhs", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.PHHS;

            textSelection = document.Find("xx_hs", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = info.HS;

            using DocIORenderer renderer = new();
            MemoryStream stream = new();
            if (downloadType == "word")
            {

                document.Save(stream, FormatType.Docx);
                stream.Position = 0;

                return File(stream, "application/docx", "TTHS.docx");
            }
            else
            {
                PdfDocument pdfDocument = renderer.ConvertToPDF(document);
                pdfDocument.Save(stream);
                stream.Position = 0;

                return File(stream, "application/pdf", "TTHS.pdf");
            }
        }
    }
}
