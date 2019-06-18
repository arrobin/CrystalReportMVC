using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared.Interop;
using CrystalReportMVC.CrystalReports;

namespace CrystalReportMVC.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerDBEntities context = new CustomerDBEntities();
        // GET: Customer
        public ActionResult Index()
        {
            var customerList = context.Customers.ToList();
            return View(customerList);
        }

        public ActionResult ExportCustomers()
        {
            List<Customer> allCustomer=new List<Customer>();
            allCustomer = context.Customers.ToList();
            //DataTable customerDataTable = new DataTable();
            //foreach (var customer in allCustomer)
            //{
            //    customerDataTable.Rows.Add(customer.CustomerID,customer.CustomerName,customer.CustomerEmail,customer.CustomerZipCode,customer.CustomerCountry,customer.CustomerCity);
            //}
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "CustomerReport.rpt"));
            rd.SetDataSource(allCustomer);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerList.pdf");
        }
    }
}