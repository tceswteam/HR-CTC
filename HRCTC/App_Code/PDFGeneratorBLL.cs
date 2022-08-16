
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

using System.Text;
using System.Globalization;

    public class PDFGeneratorBLL
    {
        PdfContentByte cb;

        public Byte[] GeneratePDFDoc(string vContent)
        {

            #region"Commented"
            
            //            string pdfTemplate =
//@"C:\PDFFileSoftware\PdfGenerator_CS\PdfGenerator_CS\PdfGenerator\1_Form SS-4 (2007).pdf";
//            //@"c:\Temp\PDF\fw4.pdf";
//            string newFile = @"c:\Temp\PDF\completed_fw4.pdf";
//            PdfReader pdfReader = new PdfReader(pdfTemplate);
//            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
//            newFile, FileMode.Create));
//            AcroFields pdfFormFields = pdfStamper.AcroFields;
//            string chartLoc = string.Empty;

//            chartLoc = @"C:\Temp\PDF\IMG_3746.jpg";//pplLogoSmall.jpg";
//            iTextSharp.ToString().Image chartImg = iTextSharp.ToString().Image.GetInstance(chartLoc);
//            iTextSharp.ToString().pdf.PdfContentByte underContent;
//            iTextSharp.ToString().Rectangle rect;
//            try
//            {
//                Single X, Y; int pageCount = 0;
//                rect = pdfReader.GetPageSizeWithRotation(1);
//                if (chartImg.Width > rect.Width || chartImg.Height > rect.Height)
//                {

//                    chartImg.ScaleToFit(rect.Width, rect.Height);

//                    X = (rect.Width - chartImg.ScaledWidth) / 2;

//                    Y = (rect.Height - chartImg.ScaledHeight) / 2;

//                }

//                else
//                {

//                    X = (rect.Width - chartImg.Width) / 2;

//                    Y = (rect.Height - chartImg.Height) / 2;

//                } chartImg.SetAbsolutePosition(X, Y);

//                pageCount = pdfReader.NumberOfPages;
//                for (int i = 1; i < pageCount; i++)
//                {

//                    underContent = pdfStamper.GetOverContent(i);//.GetUnderContent(i);

//                    underContent.AddImage(chartImg);

//                }

//                pdfStamper.Close();

//                pdfReader.Close();

//            }
//            catch (Exception ex)
//            {
//                throw ex;
            //            }

            #endregion

            Document doc = null;
            MemoryStream mStream = null;
            Byte[] mData = null;
            doc = new Document(PageSize.A4, 80, 50, 30, 65);
            mStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, mStream);            
            doc.Open();

            iTextSharp.text.pdf.draw.LineSeparator line1 = new iTextSharp.text.pdf.draw.LineSeparator(0f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 1);
            doc.Add(new Chunk(line1));

            #region"CodeNotInUseButCanBeUsedInFuture"

                
            //PdfPTable table = new PdfPTable(5);
            //table.TotalWidth = 510f;//table size
            //table.LockedWidth = true;
            //table.SpacingBefore = 10f;//both are used to mention the space from heading
            //table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //table.DefaultCell.Border = PdfPCell.LEFT_BORDER;
            //table.AddCell(new Phrase("    SL.NO"));
            //table.AddCell(new Phrase("   SUBJECTS"));
            //table.AddCell(new Phrase("   MARKS"));
            //table.AddCell(new Phrase("   MAX MARK"));
            //table.AddCell(new Phrase("   CLASS AVG"));
            //doc.Add(table);


            /*StyleSheet mCss;
            class=A1,attr=border,val=1;
            mCss.LoadTagStyle(
            */

            ////string imageURL = HttpContext.Current.Request.MapPath(@"\Layout\Images\newQuest.png");

            ////iTextSharp.ToString().Image jpg = iTextSharp.ToString().Image.GetInstance(imageURL);

            //////Resize image depend upon your need

            ////jpg.SetAbsolutePosition(0, 0);
            ////jpg.ScaleToFit(140f, 120f);

            //////Give space before image

            ////jpg.SpacingBefore = 10f;

            //////Give some space after the image

            ////jpg.SpacingAfter = 1f;

            ////jpg.Alignment = Element.ALIGN_LEFT;



            //////// jpg.SetAbsolutePosition(100, 0);
            //////// jpg.ScaleAbsoluteHeight(doc.PageSize.Height);
            //////// jpg.ScaleAbsoluteWidth(doc.PageSize.Width);
            //////// doc.AddImage(jpg);
            //////// doc.NewPage();
            //////// doc.Add(jpg);

            #endregion

            System.Text.StringBuilder mBuilder = new System.Text.StringBuilder();
            TextWriter mTextWriter = new StringWriter(mBuilder);
            HtmlTextWriter mWriter = new HtmlTextWriter(mTextWriter);
            mBuilder = new System.Text.StringBuilder();
            mBuilder.Append(vContent);
            TextReader mReader = new StringReader(mBuilder.ToString());
            List<IElement> mList = HTMLWorker.ParseToList(mReader, new StyleSheet());
            foreach (IElement elm in mList)
            {
                doc.Add(elm);
                if (elm.Chunks.Count == 1)
                {
                    doc.NewPage();
                }
            }
            doc.Close();
            mData = mStream.ToArray();
            mStream.Close();
            return mData;
        }

        public void GetPDF(DataSet ds )
        {
            try
            {
                if (ds ==null)
                {
                    return;
                }
                else
                {
                //var path = SPUtility.GetGenericSetupPath(@"TEMPLATE\LAYOUTS\TDS\HTMLPrintTemplate\" + TemplateFileName);

                //HttpContext req = new HttpContext();
                //    var path = HttpContext.Current.Request.MapPath(@"PrintoutTemplate\CTC_HtmlTemplate1.html");
                var path = HttpContext.Current.Request.MapPath(@"PrintoutTemplate\CTCTemplate.html");
                StringBuilder stBl = new StringBuilder();
                StreamReader strRd = new StreamReader(path);
                stBl.Append(strRd.ReadToEnd());

                NumberFormatInfo nfo = new NumberFormatInfo();
                nfo.CurrencyGroupSeparator = ",";
                // you are interested in this part of controlling the group sizes
                nfo.CurrencyGroupSizes = new int[] { 3, 2 };
                nfo.CurrencySymbol = "";

                //NumberFormatInfo nfo = new NumberFormatInfo();
                //nfo.CurrencyGroupSeparator = ",";
                //// you are interested in this part of controlling the group sizes
                //nfo.CurrencyGroupSizes = new int[] { 3, 2 };
                //nfo.CurrencySymbol = "";

                //lblBasicExisting.ToString() = (Convert.ToInt64(lblBasicExisting.ToString()).ToString("c0", nfo)); // prints 1,50,00,000
         

                if (ds.Tables[0].Rows.Count > 0)
                {
                    stBl.Replace("#lblEmployeeName#",ds.Tables[0].Rows[0]["EmpName"].ToString());

                    stBl.Replace("#lblEmpCode#", ds.Tables[0].Rows[0]["Grade"].ToString());
                    stBl.Replace("#lblGrade#", ds.Tables[0].Rows[0]["EmpID"].ToString());
                    stBl.Replace("#lblEmpPanCard#", ds.Tables[0].Rows[0]["Emp_PAN"].ToString());
                }

                if (ds.Tables[4].Rows.Count > 0)
                {
                    var dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Basic").FirstOrDefault();
                    stBl.Replace("#lblBasicFinal#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string Basic = dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "HRA").FirstOrDefault();
                    stBl.Replace("#lblHRA#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string HRA=  dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CA").FirstOrDefault();
                    stBl.Replace("#lblCA#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo))); 
                    string CA= dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "LTA").FirstOrDefault();
                    stBl.Replace("#lblLTA#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string LTA = dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CEA").FirstOrDefault();
                    stBl.Replace("#lblCEA#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));      
                    string CEA= dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "MA").FirstOrDefault();
                    stBl.Replace("#lblMA#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string MA = dataRow["Emp_Component_Value"].ToString();
                    

                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Mobile").FirstOrDefault();
                    string Mobile = dataRow["Emp_Component_Value"].ToString();
                    if (dataRow != null)
                    {
                        stBl.Replace("#lblMobile#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                        //   lblMobileCeilingForEmp.ToString() = dataRow["CeilingLimitForEmployee"].ToString();
                    }
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "AA").FirstOrDefault();                    
                    stBl.Replace("#lblAA#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string AA = dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "PF").FirstOrDefault();                    
                    stBl.Replace("#lblPFExisting#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string PF = dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Gratuity").FirstOrDefault();
                    stBl.Replace("#lblGratuityExisting#",(Convert.ToInt64( dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string Gratuity = dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SN").FirstOrDefault();
                    stBl.Replace("#lblSNExisting#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string SN = dataRow["Emp_Component_Value"].ToString();

                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SA").FirstOrDefault();
                    stBl.Replace("#lblSiteAllowaExisting#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string SA  = dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Bonus").FirstOrDefault();
                    stBl.Replace("#lblBonusExisting#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string Bonus = dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "Canteen Subsidy").FirstOrDefault();
                    stBl.Replace("#lblCanteenExisting#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string canteensubsidy = dataRow["Emp_Component_Value"].ToString();

                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "IC").FirstOrDefault();
                    stBl.Replace("#lblICExisting#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string IC   = dataRow["Emp_Component_Value"].ToString();
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "CC").FirstOrDefault();
                    stBl.Replace("#lblCCExisting#", (Convert.ToInt64(dataRow["Emp_Component_Value"]).ToString("c0", nfo)));
                    string CC = dataRow["Emp_Component_Value"].ToString();

                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SNMAA").FirstOrDefault();                    
                    string SNMAA = dataRow["Emp_Component_Value"].ToString();

                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SNTRFAA").FirstOrDefault();                   
                    string SNTRFAA = dataRow["Emp_Component_Value"].ToString();

                   
                    int cnt_initial, cnt_end;
                    if (SNMAA != "0" || SNTRFAA != "0")
                    {
                        if (SNMAA != "")
                        {
                            stBl.Replace("#AditionalSupersuperanuation#", "");
                            stBl.Replace("lblAA_SN", (Convert.ToInt64(SNMAA).ToString("c0", nfo)));
                            stBl.Replace("#AditionalSupersuperanuation#", "Whole Superanuation Merged with AA");
                        }
                        if (SNTRFAA != "")
                        {
                            stBl.Replace("#AditionalSupersuperanuation#", "");
                            stBl.Replace("lblAA_SN", (Convert.ToInt64(SNTRFAA).ToString("c0", nfo)));
                            stBl.Replace("#AditionalSupersuperanuation#", "Addition Superanuation Merged With AA");
                        }
                    }
                    else if (SNMAA == "0" && SNTRFAA == "0")
                    {
                        cnt_initial = stBl.ToString().IndexOf("SNAA_start");
                        cnt_end = stBl.ToString().IndexOf("SNAA_end");
                        stBl.Remove(cnt_initial, cnt_end - cnt_initial);
                        stBl.Replace("SNAA_start", "");
                        stBl.Replace("SNAA_end", "");

                    }
                    else
                    {
                        cnt_initial = stBl.ToString().IndexOf("SNAA_start");
                        cnt_end = stBl.ToString().IndexOf("SNAA_end");
                        stBl.Remove(cnt_initial, cnt_end - cnt_initial);
                        stBl.Replace("SNAA_start", "");
                        stBl.Replace("SNAA_end", "");

 
                    }
                    dataRow = ds.Tables[4].AsEnumerable().Where(x => x.Field<string>("ComponentAbrev") == "SNTRFNPS").FirstOrDefault();               
                    string SNTRFNPS = dataRow["Emp_Component_Value"].ToString();

                    if (SNTRFNPS != "")
                    {                        
                        stBl.Replace("lblNPS", (Convert.ToInt64(SNTRFNPS).ToString("c0", nfo)));
                    }
                    else
                    {
                        cnt_initial = stBl.ToString().IndexOf("nps_start");
                        cnt_end = stBl.ToString().IndexOf("nps_end");
                        stBl.Remove(cnt_initial, cnt_end - cnt_initial);
                        stBl.Replace("nps_start", "");
                        stBl.Replace("nps_end", "");
                    }

                    string GroupATotalRestruc =
                           Convert.ToString(
                           Convert.ToInt64(Basic.ToString() == "" ? "0" : Basic.ToString()) +
                           Convert.ToInt64(HRA.ToString() == "" ? "0" : HRA.ToString()) +
                           Convert.ToInt64(CA.ToString() == "" ? "0" : CA.ToString()) +
                           Convert.ToInt64(LTA.ToString() == "" ? "0" : LTA.ToString()) +
                           Convert.ToInt64(CEA.ToString() == "" ? "0" : CEA.ToString()) +
                           Convert.ToInt64(MA.ToString() == "" ? "0" : MA.ToString()) +
                           Convert.ToInt64(Mobile.ToString() == "" ? "0" : Mobile.ToString()) +
                           Convert.ToInt64(AA.ToString() == "" ? "0" : AA.ToString()));
                    stBl.Replace("#lblGroupATotalExisting#",Convert.ToInt64( GroupATotalRestruc).ToString("c0", nfo));

                    string GroupBTotalRestruc =
                                     Convert.ToString(Convert.ToInt64(PF.ToString() == "" ? "0" : PF.ToString()) +
                                     Convert.ToInt64(Gratuity.ToString() == "" ? "0" : Gratuity.ToString()) +
                                     Convert.ToInt64(SN.ToString() == "" ? "0" : SN.ToString()));
                    stBl.Replace("#lblGroupBTotalExisting#", Convert.ToInt64(GroupBTotalRestruc).ToString("c0", nfo));

                    string GroupCTotalRestruc =
                                     Convert.ToString(Convert.ToInt64(SA.ToString() == "" ? "0" : SA.ToString()) +
                                     Convert.ToInt64(Bonus.ToString() == "" ? "0" : Bonus.ToString()));
                    stBl.Replace("#lblGroupCTotalExisting#", Convert.ToInt64(GroupCTotalRestruc).ToString("c0", nfo));

                    string ComputedFixedRestructur = 
                                     Convert.ToString((
                                     Convert.ToInt64(GroupATotalRestruc.ToString() == "" ? "0" : GroupATotalRestruc.ToString()) +
                                     Convert.ToInt64(GroupBTotalRestruc.ToString() == "" ? "0" : GroupBTotalRestruc.ToString()) +
                                     Convert.ToInt64(GroupCTotalRestruc.ToString() == "" ? "0" : GroupCTotalRestruc.ToString()) +
                                     Convert.ToInt64(canteensubsidy.ToString() == "" ? "0" : canteensubsidy.ToString())) * 12);
                    stBl.Replace("#lblComputedFixedExisting#", Convert.ToInt64(ComputedFixedRestructur).ToString("c0", nfo));

                    string TotalABCDRestruc =
                                  Convert.ToString(Convert.ToInt64(GroupATotalRestruc.ToString() == "" ? "0" : GroupATotalRestruc.ToString()) +
                                     Convert.ToInt64(GroupBTotalRestruc.ToString() == "" ? "0" : GroupBTotalRestruc.ToString()) +
                                     Convert.ToInt64(GroupCTotalRestruc.ToString() == "" ? "0" : GroupCTotalRestruc.ToString()) +
                                     Convert.ToInt64(canteensubsidy.ToString() == "" ? "0" : canteensubsidy.ToString())
                                     );
                    stBl.Replace("#lblTotalABCDExisting#", Convert.ToInt64(TotalABCDRestruc).ToString("c0", nfo));

                    string TotalERestructur = Convert.ToString(Convert.ToInt64(IC.ToString() == "" ? "0" : IC.ToString()) +
                                     Convert.ToInt64(CC.ToString() == "" ? "0" : CC.ToString())
                                     );
                    stBl.Replace("#lblTotalEExisting#", Convert.ToInt64(TotalERestructur).ToString("c0", nfo));


                    string ComputedCTCRestruct = Convert.ToString((Convert.ToInt64(ComputedFixedRestructur.ToString() == "" ? "0" : ComputedFixedRestructur.ToString()) +
                        Convert.ToInt64(TotalERestructur.ToString() == "" ? "0" : TotalERestructur.ToString())));
                    stBl.Replace("#lblComputedCTCExisting#",Convert.ToInt64( ComputedCTCRestruct).ToString("c0", nfo));


                }
                //stBl.Replace("#lblBasicExisting#",
                //stBl.Replace("#lblHRAExisting#",
                //stBl.Replace("#lblConAlloExisting#",
                //stBl.Replace("#lblLTAExisting#",
                //stBl.Replace("#lblCEAExisting#",
                //stBl.Replace("#lblMAExisting#",
                //stBl.Replace("#lblMobileExisting#",
                //stBl.Replace("#lblAAExisting#",
                //stBl.Replace("#lblAASuperAnuationOption#",
                //stBl.Replace("#lblGroupATotalExisting#",
                //stBl.Replace("#lblPFExisting#",
                //stBl.Replace("#lblGratuityExisting#",
                //stBl.Replace("#lblSNExisting#",
                //stBl.Replace("#lblGroupBTotalExisting#",
                //stBl.Replace("#lblSiteAllowaExisting#",
                //stBl.Replace("#lblBonusExisting#",
                //stBl.Replace("#lblGroupCTotalExisting#",
                //stBl.Replace("#lblCanteenExisting#",
                //stBl.Replace("#lblTotalABCDExisting#",
                //stBl.Replace("#lblComputedFixedExisting#",
                //stBl.Replace("#lblICExisting#",
                //stBl.Replace("#lblCCExisting#",
                //stBl.Replace("#lblTotalEExisting#",
                //stBl.Replace("#lblComputedCTCExisting#",

                PDFGeneratorBLL objPDFGeneratorBLL = new PDFGeneratorBLL();
                byte[] mFileData = null;
                mFileData = GeneratePDFDoc(stBl.ToString());
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", "GenerateHTMLTOPDF.pdf"));
                HttpContext.Current.Response.OutputStream.Write(mFileData, 0, mFileData.Length);
                HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                    
                }
            }
            catch (Exception ex )
            {
                string strErr = ex.Message;
                throw;
            }
         
        }

        public string DumpHtmlDs(string msg, DataSet ds)
        {
            StringBuilder objStringBuilder = new StringBuilder();
            objStringBuilder.AppendLine("");
            if (ds == null)
            {
                objStringBuilder.AppendLine("Null dataset passed ");
                objStringBuilder.AppendLine("</html></body>");
                //WriteIf(objStringBuilder.ToString());
                return objStringBuilder.ToString();
            }
            //  objStringBuilder.AppendLine("<p>" + msg + " START </p>");
            if (ds != null)
            {
                if (ds.Tables == null)
                {
                    objStringBuilder.AppendLine("ds.Tables == null ");
                    return objStringBuilder.ToString();
                }
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    if (dt == null)
                    {
                        objStringBuilder.AppendLine("ds.Tables == null ");
                        continue;
                    }
                    objStringBuilder.AppendLine("<table border=1  >");
                    //objStringBuilder.AppendLine("================= My TableName is  " +                 //dt.TableName + " ========================= START");                 
                    int colNumberInRow = 0;
                    objStringBuilder.Append("<tr >");
                    foreach (System.Data.DataColumn dc in dt.Columns)
                    {
                        if (dc == null)
                        {
                            objStringBuilder.AppendLine("DataColumn is null ");
                            continue;
                        }
                        objStringBuilder.Append(" <th style=' font-weight: bold;'> " + dc.ColumnName.ToString() + " </th> ");
                        //objStringBuilder.Append(dc.ColumnName.ToString() + " </th> ");
                        colNumberInRow++;
                    }
                    //eof foreach (DataColumn dc in dt.Columns)    
                    objStringBuilder.Append("</tr>");
                    int rowNum = 0;
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        //objStringBuilder.Append("<tr><td> row - | " + rowNum.ToString() + " | </td>");
                        objStringBuilder.Append("<tr>");
                        int colNumber = 0;
                        foreach (System.Data.DataColumn dc in dt.Columns)
                        {
                            objStringBuilder.Append(" <td> ");
                            objStringBuilder.Append(dr[dc].ToString() + "  </td>");
                            colNumber++;
                        } //eof foreach (DataColumn dc in dt.Columns)   
                        rowNum++;
                        objStringBuilder.AppendLine(" </tr>");
                    }
                    //eof foreach (DataRow dr in dt.Rows)   
                    objStringBuilder.AppendLine("</table>");
                    //   objStringBuilder.AppendLine("<p>" + msg + " END </p>");
                }

                return objStringBuilder.ToString();
            }
            return null;
        }

    }

