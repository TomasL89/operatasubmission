using System;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;

namespace LogSubmissionPortal.Models
{
    public static class AWSConnection
    {
        private const string bucketName = "logmessagebucket";
        private const string accessKey = "Enter your access key here";
        private const string secretAccessKey = "Enter your secret access key here";

        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSoutheast2;
        private static IAmazonS3 s3Client;

        public static async Task<bool> UploadLogFileAsync(Log log)
        {
            var couldUpload = false;
            try
            {
                s3Client = new AmazonS3Client(accessKey, secretAccessKey, bucketRegion);

                var contentBody = CreateJsonPayload(log);
                var putRequest1 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = log.Subject + ".log",
                    ContentBody = contentBody
                };
                PutObjectResponse response = await s3Client.PutObjectAsync(putRequest1);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                    couldUpload = true;
            }
            catch (Exception)
            {
                couldUpload = false;
            }

            return couldUpload;
        }

        public static string CreateJsonPayload(Log log)
        {
            return JsonConvert.SerializeObject(log);
        }
    }
}