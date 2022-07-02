using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.AcroForms;
using PdfSharpCore.Pdf.IO;

var inputPath = "../../../SampleFormInput.pdf";
var outputPath = "../../../SampleFormOutput.pdf";

PdfDocument myTemplate = PdfReader.Open(inputPath, PdfDocumentOpenMode.Modify);
PdfAcroForm form = myTemplate.AcroForm;

ConfigureForm(form);

// Does not work
//var typeGroup = (PdfRadioButtonField)form.Fields["TypeGroup"];
//typeGroup.Value = new PdfName("/Approval");


var nameField = (PdfTextField)(form.Fields["NameField"]);
nameField.Value = new PdfString("Ivan Brevenko");


var dateField = (PdfTextField)(form.Fields["DateField"]);
dateField.Value = new PdfString("2022/06/11");


var explanationField = (PdfTextField)(form.Fields["ExplanationField"]);
explanationField.Value = new PdfString("There is nothing to say");

myTemplate.Save(outputPath);


/// this functions has to be applied to a form
/// it makes fields visible after editing
void ConfigureForm(PdfAcroForm form)
{
    if (form.Elements.ContainsKey("/NeedAppearances"))
    {
        form.Elements["/NeedAppearances"] = new PdfBoolean(true);
    }
    else
    {
        form.Elements.Add("/NeedAppearances", new PdfBoolean(true));
    }
}

// use the following link to make radio buttons work
// https://stackoverflow.com/questions/48231834/populate-pdfradiobuttonfield-using-pdfsharp