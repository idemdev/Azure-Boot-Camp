#
r "Microsoft.WindowsAzure.Storage"#
r "Newtonsoft.Json"

using System;
using System.Net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

public static async Task < object > Run(HttpRequestMessage req, TraceWriter log) {
 log.Info($ "Webhook was triggered!");

 int ? discount = 0;
 string accountName = "gab2017aklstorws";
 string accountKey = "LVBPX6ODPz6wYvwjm1weHx5ehvFB9TjU5TZ2l5IavTRkgghyO+t73j8KIZn6ndFSz0QDhaieSfgSh6miDnytrQ==";

 string jsonContent = await req.Content.ReadAsStringAsync();
 dynamic data = JsonConvert.DeserializeObject(jsonContent);

 string company = data.Company;

 // Here we will connect to our storage account
 try {
  string storageAccountConnectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", accountName, accountKey);
  CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageAccountConnectionString);
  CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
  CloudTable table = tableClient.GetTableReference("CustomerDiscount");

  //Create a filter expression
  var tableQuery = new TableQuery < DynamicTableEntity > ();
  tableQuery.FilterString = TableQuery.CombineFilters(
   TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Company"),
   TableOperators.And,
   TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, company));

  // Loop through the results, displaying information about the entity.
  foreach(DynamicTableEntity entity in table.ExecuteQuery(tableQuery)) {
   var item = entity.Properties;
   discount = item["Discount"].Int32Value;
  }
 } catch (Exception ex) {
  log.Info($ "Webhook exception :" + ex.Message);
 }

 return req.CreateResponse(HttpStatusCode.OK, new {
  discountValue = discount
 });
}