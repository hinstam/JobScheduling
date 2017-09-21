using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.Common;
using System.IO;
using NPOI;
using NPOI.XSSF.UserModel;

namespace JobScheduling.Business.SchedulingBL
{
    public class ProductSchedulingBL
    {
        public IList<int> GetProductSchedulingData()
        {
            return null;
        }

        public bool SaveExportProductScheduling(string tplsFilePath, string exportFilePath)
        {
            var fs = new FileStream(tplsFilePath, FileMode.Open);

                XSSFWorkbook wb = new XSSFWorkbook(fs);

                XSSFSheet sheet = wb.GetSheet("sheet1") as XSSFSheet;

                FileStream exportFs = File.Create(exportFilePath);

                wb.Write(exportFs);

                exportFs.Flush();
                exportFs.Close();

            return true;
        }
    }
}
