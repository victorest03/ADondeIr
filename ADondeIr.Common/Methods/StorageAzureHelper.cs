namespace ADondeIr.Common.Methods
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net.Mime;
    using System.Web;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public static class StorageAzureHelper
    {
        private static readonly string AzureStorageConnection = ConfigurationManager.ConnectionStrings["AzureStorageConnection"]?.ConnectionString;

        public static Stream Get(string container, string filename, out string contentType)
        {
            var stream = new MemoryStream();
            var storageAccount = CloudStorageAccount.Parse(AzureStorageConnection);
            var client = storageAccount.CreateCloudBlobClient();
            if (client.GetContainerReference(container)
                .GetBlockBlobReference(filename)
                .Exists())
            {
                var blockblob = client.GetContainerReference(container).GetBlockBlobReference(filename);
                blockblob.FetchAttributes();
                contentType = blockblob.Properties.ContentType;
                client.GetContainerReference(container).GetBlockBlobReference(filename).DownloadToStream(stream);
                stream.Position = 0;
            }
            else
            {
                contentType = null;
                stream = null;
            }

            return stream;
        }

        public static bool Save(string containerName, string fileName, Stream fileMemoryStream)
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(AzureStorageConnection);
                var client = storageAccount.CreateCloudBlobClient();

                var container = client.GetContainerReference(containerName);
                var blob = container.GetBlockBlobReference(fileName);
                blob.UploadFromStream(fileMemoryStream);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GenerateNameFile(this HttpPostedFileBase file)
        {
            return $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
        }
    }
}
