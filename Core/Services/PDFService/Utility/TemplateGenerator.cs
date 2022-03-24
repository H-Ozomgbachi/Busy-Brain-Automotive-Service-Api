using Common.Core.Models.StandardRepairFeatureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Core.Services.PDFService.Utility
{
    public static class TemplateGenerator
    {
        public static string StandardRepairResultHTMLString(List<StandardRepairResult> standardRepairResults)
        {
            var sb = new StringBuilder();
            var currentDate = DateTime.Now.ToString("MMMM dd, yyyy. hh:mm:ss tt");

            sb.AppendFormat(@"
                <html>
                    <head>
                    </head>

                    <body>
                        <div class='header'>
                            <h1>Busy Brain Automotive Services</h1>
                            <h3>Standard Maintenance Invoice</h3>
                            <h5>Date Printed : {0}</h5>
                        </div>
                        <table align='center'>
                            <tr>
                                <th>Maintenance Item</th>
                                <th>Code</th>
                                <th>Amount</th>
                            </tr>", currentDate);

            foreach (var item in standardRepairResults)
            {
                sb.AppendFormat(@"
                                    <tr>
                                        <td>{0}</td>
                                        <td>{1}</td>
                                        <td>₦ {2}</td>
                                    </tr>
                                ", item.RepairName, item.RepairCode, string.Format("{0:n}", item.RepairAmount));
            }
            sb.AppendFormat(@"
                        <tr>
                            <td></td>
                            <td class='standard-repair-table-footer'>Total</td>
                            <td class='standard-repair-table-footer'>₦ {0}</td>
                        </tr>", string.Format("{0:n}", standardRepairResults.Sum(x => x.RepairAmount)));

            sb.Append(@"
                        </table>
                    </body>
                </html>");
            return sb.ToString();
        }
    }
}