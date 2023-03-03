using System;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp1
{
    class Report
    {
        Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

        Word.Document doc;

        ~Report()
        {
            doc.Saved = true;
            try { app.Quit(); }
            catch { }
        }

        public void OtchetEffect(IList<Sale> sales)
        {
            Dictionary<int, int> Mounth = new Dictionary<int, int>(12);
            for (int i = 1; i <= 12; i++)
            {
                Mounth.Add(i, 0);
            }

            if (sales != null)
            {
                doc = app.Documents.Add(Template: $@"{Environment.CurrentDirectory}\Templates\Отчетобэффективности.docx", Visible: true);

                Word.Range dateTime = doc.Bookmarks["DateTime"].Range;
                dateTime.Text = DateTime.Now.ToString();

                Word.Table table = doc.Bookmarks["Table"].Range.Tables[1];
                int currPage = 1;
                foreach (var item in sales)
                {                
                   switch (item.Date.Month.ToString())
                    {
                        case "1":
                            Mounth[1] += item.Quantity;
                            break;
                        case "2":
                            Mounth[2] += item.Quantity;
                            break;
                        case "3":
                            Mounth[3] += item.Quantity;
                            break;
                        case "4":
                            Mounth[4] += item.Quantity;
                            break;
                        case "5":
                            Mounth[5] += item.Quantity;
                            break;
                        case "6":
                            Mounth[6] += item.Quantity;
                            break;
                        case "7":
                            Mounth[7] += item.Quantity;
                            break;
                        case "8":
                            Mounth[8] += item.Quantity;
                            break;
                        case "9":
                            Mounth[9] += item.Quantity;
                            break;
                        case "10":
                            Mounth[10] += item.Quantity;
                            break;
                        case "11":
                            Mounth[11] += item.Quantity;
                            break;
                        case "12":
                            Mounth[12] += item.Quantity;
                            break;

                    }
   


                }

                for(int i = 1;i<=12; i++)
                {
                    
                    if (Mounth[i]>0)
                    {
                        int page = doc.ComputeStatistics(Word.WdStatistic.wdStatisticPages);

                        Word.Row row = table.Rows.Add();
                        if (page > currPage) //Если запись не влазеет на текущею страницу
                        {
                            row.Range.InsertBreak();
                            table = doc.Tables[doc.Tables.Count];

                            doc.Tables[1].Rows[1].Range.Copy();
                            row.Range.Paste();
                            table.Rows[2].Delete(); //Удаляем пустую строку после заголовка

                            currPage = page;
                            row = table.Rows.Add();
                        }

                        row.Cells[1].Range.Text = convertMonth(i.ToString());
                        row.Cells[2].Range.Text = Mounth[i].ToString();
                    }
                }
                
                doc.Bookmarks["Table"].Range.Tables[1].Rows[2].Delete();

                app.Visible = true;
            }
        }

        private string convertMonth(string mounth)
        {
            switch (mounth)
            {
                case "1":
                    return "Январь";
                case "2":
                    return "Февраль";
                case "3":
                    return "Март";
                case "4":
                    return "Апрель";
                case "5":
                    return "Май";
                case "6":
                    return "Июнь";
                case "7":
                    return "Июль";
                case "8":
                    return "Август";
                case "9":
                    return "Сентябрь";
                case "10":
                    return "Октябрь";
                case "11":
                    return "Ноябрь";
                case "12":
                    return "Декабрь";
                default:
                    return "a";
            }

        }

        public void VsePokypkiPokypatelia(IList<Sale> sales)
        {
          /*  if (sales != null)
            {
                Excel.Application app = new Excel.Application
                {
                    //Отобразить Excel
                    Visible = true,
                    //Количество листов в рабочей книге
                    SheetsInNewWorkbook = 1
                };
                //Добавить рабочую книгу
                Excel.Workbook workBook = app.Workbooks.Add(Type.Missing);
                //Отключить отображение окон с сообщениями
                app.DisplayAlerts = false;
                //Получаем первый лист документа (счет начинается с 1)
                Excel.Worksheet sheet = (Excel.Worksheet)app.Worksheets.get_Item(1);
                //Название листа (вкладки снизу)
                sheet.Name = "Имя должно быть не больше 32сим";
                foreach (var item in sales) 
                {
                    Dictionary<int, int, string> Mounth = new Dictionary<int, int, string>(12);
                }
                    for (int i = 1; i <= 9; i++)
                {
                    for (int j = 1; j < 9; j++)
                        sheet.Cells[i, j] = String.Format("nookery {0} {1}", i, j);
                }
            }*/
        }

        public void Sells(IList<Sale> salle)
        {
            if (salle != null)
            {
                int quantSell = 0;
                decimal Summ = 0;
                doc = app.Documents.Add(Template: $@"{Environment.CurrentDirectory}\Templates\ОтчетПродажи.docx", Visible: true);

                Word.Range dateTime = doc.Bookmarks["DateTime"].Range;
                dateTime.Text = DateTime.Now.ToString();

                Word.Table table = doc.Bookmarks["Table"].Range.Tables[1];
                int currPage = 1;
                foreach (var item in salle)
                {
                    int page = doc.ComputeStatistics(Word.WdStatistic.wdStatisticPages);

                    Word.Row row = table.Rows.Add();
                    if (page > currPage) //Если запись не влазеет на текущею страницу
                    {
                        row.Range.InsertBreak();
                        table = doc.Tables[doc.Tables.Count];

                        doc.Tables[1].Rows[1].Range.Copy();
                        row.Range.Paste();
                        table.Rows[2].Delete(); //Удаляем пустую строку после заголовка

                        currPage = page;
                        row = table.Rows.Add();
                    }

                    row.Cells[1].Range.Text = item.Date.ToString();
                    row.Cells[2].Range.Text = item.Salesman.Name;
                    row.Cells[3].Range.Text = item.Customer.Name;
                    row.Cells[4].Range.Text = item.Product.Name;
                    row.Cells[5].Range.Text = item.Product.Price.ToString();
                    row.Cells[6].Range.Text = item.Quantity.ToString();
                    row.Cells[7].Range.Text = item.Sum.ToString();
                    quantSell += item.Quantity;
                    Summ += item.Sum;
                }
                Word.Range qantSells = doc.Bookmarks["summs"].Range;
                qantSells.Text = quantSell.ToString();
                Word.Range Summs = doc.Bookmarks["sells"].Range;
                Summs.Text = Summ.ToString();
                doc.Bookmarks["Table"].Range.Tables[1].Rows[2].Delete(); //Удаляем строку [текст] [текст] [текст] [текст] в таблице

                app.Visible = true;
            }
        }
    }
}
