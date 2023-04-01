namespace com.quinncurtis.chart2dnet
{
    using System;

    public class ChartException : Exception
    {
        private string ExceptionMessage;

        public ChartException()
        {
        }

        public ChartException(string s) : base(s)
        {
            this.ExceptionMessage = s;
        }

        public string GetExceptionMessage()
        {
            return this.ExceptionMessage;
        }
    }
}

