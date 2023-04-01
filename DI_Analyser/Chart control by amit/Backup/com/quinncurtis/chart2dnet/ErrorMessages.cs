namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Collections;

    public class ErrorMessages : ChartObj
    {
        private static ArrayList errorStrings;

        public ErrorMessages()
        {
            this.InitDefaults();
        }

        public override object Clone()
        {
            return new ErrorMessages();
        }

        public string GetErrorMessage(int errorcode)
        {
            string errorMessage = string.Copy("");
            ErrorRecord record = this.GetRecord(errorcode);
            if (record != null)
            {
                errorMessage = record.errorMessage;
            }
            return errorMessage;
        }

        private ErrorRecord GetRecord(int errorcode)
        {
            ErrorRecord record = null;
            int count = errorStrings.Count;
            for (int i = 0; i < count; i++)
            {
                record = (ErrorRecord) errorStrings[i];
                if (record.errorNumber == errorcode)
                {
                    return record;
                }
            }
            return null;
        }

        private void InitDefaults()
        {
            errorStrings = new ArrayList(100);
            errorStrings.Add(new ErrorRecord("No Error", "ERROR_NONE", 0));
            errorStrings.Add(new ErrorRecord("Component error", "ERROR_COMPONENT", 10));
            errorStrings.Add(new ErrorRecord("A scaling object is null", "ERROR_SCALING", 20));
            errorStrings.Add(new ErrorRecord("A scale range is considered invalid - minimum and maximum values the same", "ERROR_SCALERANGE", 30));
            errorStrings.Add(new ErrorRecord("Null viewport", "ERROR_NULLVIEWPORT", 50));
            errorStrings.Add(new ErrorRecord("Viewport to small", "ERROR_SMALLVIEWPORT", 0x33));
            errorStrings.Add(new ErrorRecord("Borders to large", "ERROR_BORDER", 60));
            errorStrings.Add(new ErrorRecord("Non-specific axis error", "ERROR_AXIS", 100));
            errorStrings.Add(new ErrorRecord("Axis labels error", "ERROR_AXISLABLES", 110));
            errorStrings.Add(new ErrorRecord("Base axis is null, object needs a valid base axis type", "ERROR_NULLBASEAXIS", 120));
            errorStrings.Add(new ErrorRecord("Object expects a different axis type (Can't use a numeric axis labels with a time axis)", "ERROR_WRONGAXISTYPE", 0x79));
            errorStrings.Add(new ErrorRecord("Axis range error - minimum and maximum the same value", "ERROR_AXISRANGE", 130));
            errorStrings.Add(new ErrorRecord("Axis tick mark spacing must be a finite, positive number, for example 0.0 is invalid tick mark spacing ", "ERROR_AXISTICKS", 140));
            errorStrings.Add(new ErrorRecord("Polar axis labels need to reference a valid polar axes object", "ERROR_POLARAXES", 150));
            errorStrings.Add(new ErrorRecord("The font for the text object is null", "ERROR_FONT", 200));
            errorStrings.Add(new ErrorRecord("A non-specific text object error", "ERROR_TEXT", 210));
            errorStrings.Add(new ErrorRecord("A non-specific time axis label error", "ERROR_TIMELABEL", 220));
            errorStrings.Add(new ErrorRecord("A non-specific symbol error", "ERROR_SYMBOL", 230));
            errorStrings.Add(new ErrorRecord("A non-specific shape error", "ERROR_SHAPE", 240));
            errorStrings.Add(new ErrorRecord("A non-specific image error", "ERROR_IMAGE", 300));
            errorStrings.Add(new ErrorRecord("A non-specific legend error", "ERROR_LEGEND", 310));
            errorStrings.Add(new ErrorRecord("The text for a legend item is null", "ERROR_LEGENDITEMTEXT", 320));
            errorStrings.Add(new ErrorRecord("The symbol for a legend item is null", "ERROR_LEGENDITEMSYMBOL", 330));
            errorStrings.Add(new ErrorRecord("A non-specific super zoom error", "ERROR_SUPERZOOM", 400));
            errorStrings.Add(new ErrorRecord("Wrong object type", "ERROR_OBJECTMISMATCH", 410));
            errorStrings.Add(new ErrorRecord("Error in creating a simple dataset", "ERROR_SIMPLEDATASET", 500));
            errorStrings.Add(new ErrorRecord("Error in creating a group dataset", "ERROR_GROUPDATASET", 510));
            errorStrings.Add(new ErrorRecord("Error in creating a contour dataset", "ERROR_CONTOURDATASET", 520));
            errorStrings.Add(new ErrorRecord(" Error in creating a dataset, probably the x-values ", "ERROR_DATASET", 530));
            errorStrings.Add(new ErrorRecord("Error in creating a dataset, probably from a file, where the number of data points is < 2, and the number of groups < 1", "ERROR_DATASETSIZE", 540));
            errorStrings.Add(new ErrorRecord("Problem allocating an array ", "ERROR_ARRAY_NEW", 600));
            errorStrings.Add(new ErrorRecord("Temporary object cannot be allocated", "ERROR_OBJ_NEW", 610));
            errorStrings.Add(new ErrorRecord("Bad polysurface", "ERROR_BAD_POLYSURFACE", 620));
            errorStrings.Add(new ErrorRecord("Error in the number of rows or columns in an evenly spaced grid", "ERROR_GRID_ROW_COL", 630));
            errorStrings.Add(new ErrorRecord("Error in the Delaunay triangularzation algorithm", "ERROR_DELAUNAY", 640));
            errorStrings.Add(new ErrorRecord(" Internal object error", "ERROR_PLOTOBJ", 700));
            errorStrings.Add(new ErrorRecord("The bar defining the position of the numeric text is invalid.", "ERROR_BARDATAVALUE", 0x2bd));
            errorStrings.Add(new ErrorRecord("Generic file i/o error", "ERROR_FILEIO", 800));
            errorStrings.Add(new ErrorRecord("File open error", "ERROR_FILEOPEN", 0x321));
            errorStrings.Add(new ErrorRecord("File write error", "ERROR_FILEWRITE", 0x322));
            errorStrings.Add(new ErrorRecord(ChartObj.ERROR_FILEREAD_STRING, "ERROR_FILEREAD", ChartObj.ERROR_FILEREAD));
            errorStrings.Add(new ErrorRecord("File close error", "ERROR_FILECLOSE", 0x324));
            errorStrings.Add(new ErrorRecord("Non-specific auto scale error", "ERROR_AUTOSCALE", 900));
            errorStrings.Add(new ErrorRecord("ChartDataset invalid in auto scale calculation", "ERROR_AUTOSCALE_DATASET", 0x385));
            errorStrings.Add(new ErrorRecord("ChartDataset array invalid in auto scale calculation", "ERROR_AUTOSCALE_DATASETSARRAY", 0x386));
            errorStrings.Add(new ErrorRecord("Transform invalid in auto scale calculation", "ERROR_AUTOSCALE_TRANSFORM", 0x387));
        }

        public void SetErrorMessage(int errorcode, string errmessage)
        {
            string.Copy("");
            ErrorRecord record = this.GetRecord(errorcode);
            if (record != null)
            {
                record.errorMessage = errmessage;
            }
        }

        private class ErrorRecord
        {
            public string errorConstantstring;
            public string errorMessage;
            public int errorNumber;

            public ErrorRecord(string errmess, string errconstantstring, int errnum)
            {
                this.errorNumber = errnum;
                this.errorMessage = errmess;
                this.errorConstantstring = errconstantstring;
            }
        }
    }
}

