using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FetchJiraConsole
{
    public class ListHelp<T>
    {
        IEnumerable<T> m_list;
        //FieldInfo[] m_fields = typeof(T).GetFields();
        PropertyInfo[] m_fields = typeof(T).GetProperties();

        public bool HasFieldnamesInHeader = true;
        public string CsvSeparator = ";";

        public ListHelp(IEnumerable<T> list)
        {
            m_list = list; 
        }

        public void CsvLinesToFile(string filename)
        {
            using (System.IO.TextWriter tw = new System.IO.StreamWriter(filename,false,Encoding.Default))
            {
                foreach (var s in CsvLines) tw.Write(s + Environment.NewLine);
                tw.Close();
            }
        }

        public string CsvLinesAsString
        {
            get
            {
                StringBuilder csvdata = new StringBuilder();
                foreach (var s in CsvLines) csvdata.AppendLine(s);
                return csvdata.ToString();
            }
            private set {}
        }

        public string CsvHeader
        { 
            private set { } 
            get { return String.Join(CsvSeparator, m_fields.Select(f => f.Name).ToArray()); } 
        }

        public IEnumerable<string> CsvLines
        {
            get
            {
                if (HasFieldnamesInHeader)
                    yield return CsvHeader;

                foreach (var o in m_list)
                    yield return FieldsToCsv(o);
            }
            private set {}
        }

        public string FieldsToCsv(object o)
        {
            StringBuilder line = new StringBuilder();

            foreach (var f in m_fields)
            {
                if (line.Length > 0)
                    line.Append(CsvSeparator);

                var x = f.GetValue(o);

                if (x != null)
                    line.Append(x.ToString());
            }

            return line.ToString();
        }
    }
}
