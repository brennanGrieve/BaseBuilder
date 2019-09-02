using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBlocker
{
    class Row
    {
        private int rowSize;
        private object[] rowData;
        private int i = -1;

        public int RowSize { get => rowSize; set => rowSize = value; }
        public object[] RowData { get => rowData; set => rowData = value; }

        public object CurrentRowItem
        {
            get
            {
                i++;
                return RowData[i];
            }
        }

    }
}
