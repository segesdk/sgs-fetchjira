using System;

namespace CsvToSqlCreate
{
    public class FieldFormat
    {
        public string Name;
        public int MinLength = int.MaxValue;
        public int MaxLength = 0;
        public double MinValue = double.MaxValue;
        public double MaxValue = double.MinValue;
        public double AvgValue = 0;
        public bool HasNull = false;

        public enum Format
        {
            Unknown, String, DateTime, Double, Int
        }

        public Format StringFormat = Format.Unknown;
        public bool HasTime = false;

        public void Parse(string s, int linecount)
        {
            if (string.IsNullOrEmpty(s))
            {
                HasNull = true;
                return;
            }

            if (s.Length > MaxLength) MaxLength = s.Length;
            if (s.Length < MinLength) MinLength = s.Length;

            
            // Hierarchy: string, int, double, datetime

            if (s.Length > 0 && StringFormat != Format.String)
            {
                int itp;
                if (int.TryParse(s, out itp))
                {
                    switch (StringFormat)
                    {
                        case Format.Int:
                            break;
                        case Format.Double:
                            break;
                        case Format.DateTime:
                            StringFormat = Format.String;
                            break;
                        case Format.Unknown:
                            StringFormat = Format.Int;
                            break;
                        default:
                            StringFormat = Format.String;
                            break;
                    }
                    if (itp < MinValue) MinValue = itp;
                    if (itp > MaxValue) MaxValue = itp;
                    //AvgValue = ((AvgValue * (linecount-1)) + itp) / (linecount); // https://math.stackexchange.com/questions/1153794/adding-to-an-average-without-unknown-total-sum
                }
                else
                {
                    double dtp;
                    if (double.TryParse(s, out dtp))
                    {
                        switch (StringFormat)
                        {
                            case Format.Int:
                                StringFormat = Format.Double;
                                break;
                            case Format.Double:
                                break;
                            case Format.DateTime:
                                StringFormat = Format.String;
                                break;
                            case Format.Unknown:
                                StringFormat = Format.Double;
                                break;
                            default:
                                StringFormat = Format.String;
                                break;
                        }
                        if (dtp < MinValue) MinValue = dtp;
                        if (dtp > MaxValue) MaxValue = dtp;
                    }
                    else
                    {
                        DateTime dttp;
                        if (DateTime.TryParse(s, out dttp))
                        {
                            switch (StringFormat)
                            {
                                case Format.Int:
                                    StringFormat = Format.String;
                                    break;
                                case Format.Double:
                                    StringFormat = Format.String;
                                    break;
                                case Format.DateTime:
                                    if (dttp.TimeOfDay.TotalSeconds > 0) HasTime = true;
                                    break;
                                case Format.Unknown:
                                    StringFormat = Format.DateTime;
                                    if (dttp.TimeOfDay.TotalSeconds > 0) HasTime = true;
                                    break;
                                default:
                                    StringFormat = Format.String;
                                    break;
                            }
                        }
                        else
                        {
                            StringFormat = Format.String;
                        }
                    }
                }
            }
        }
    }
}