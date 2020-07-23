using System;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Threading.Tasks;

namespace AWSConnection
{
    internal class Program
    {
        private const string bucketName = "Enter test bucket name here";

        private const string filePath = @"Enter test file path here";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSoutheast2;

        private static IAmazonS3 s3Client;

        private static async Task Main(string[] args)
        {
            s3Client = new AmazonS3Client(bucketRegion);
            var buckets = await s3Client.ListBucketsAsync();
            UploadFileAsync().Wait();
        }

        private static async Task UploadFileAsync()
        {
            try
            {
                var fileTransferUtility =
                    new TransferUtility(s3Client);

                // Option 1. Upload a file. The file name is used as the object key name.
                await fileTransferUtility.UploadAsync(filePath, bucketName);
                Console.WriteLine("Upload 1 completed");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }
    }
}