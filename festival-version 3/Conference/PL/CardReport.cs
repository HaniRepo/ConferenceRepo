using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;


namespace Conference.PL
{
    public partial class CardReport : Form
    {
        string name;
        string title;
        string code;
        string isVip;
        string ename;
        string etitle;
        public CardReport(string _name,string _title,string _code,string _isVip,string _ename,string _etitle)
        {
            name=_name;
            title=_title;
            code=_code;
            isVip=_isVip;
            ename = _ename;
            etitle = _etitle;
            InitializeComponent();
        }

        private void CardReport_Load(object sender, EventArgs e)
        {
             ReportParameter rpName=new ReportParameter("rpName",name);
            ReportParameter rpTitle=new ReportParameter("rpTitle",title);
            ReportParameter rpBarcode=new ReportParameter("rpBarcode","*"+code+"*");
            ReportParameter rpCode=new ReportParameter("rpCode",code);
            ReportParameter rpisVIP=new ReportParameter("rpisVIP",isVip);
            ReportParameter rpename = new ReportParameter("rpename", ename);
            ReportParameter rpetitle = new ReportParameter("rpetitle", etitle);
            reportViewer1.LocalReport.SetParameters(rpName);
            reportViewer1.LocalReport.SetParameters(rpTitle);
            reportViewer1.LocalReport.SetParameters(rpBarcode);
            reportViewer1.LocalReport.SetParameters(rpCode);
            reportViewer1.LocalReport.SetParameters(rpisVIP);
            reportViewer1.LocalReport.SetParameters(rpename);
            reportViewer1.LocalReport.SetParameters(rpetitle);
            this.reportViewer1.RefreshReport();
        }
    }
}
