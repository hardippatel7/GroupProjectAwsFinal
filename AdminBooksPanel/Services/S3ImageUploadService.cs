namespace PhotoAlbumApi.Services
{
    using AdminBooksPanel.Models;
    using Amazon;
    using Amazon.S3;
    using Amazon.S3.Model;
    using Amazon.S3.Transfer;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class S3ImageUploadService : IS3ImageUploadService
    {
        private readonly string accessKey = $"AKIAYAEISDM4U35SPMAR";

        private readonly string secretKey = $"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";

        private readonly string bucketName = $"s3bucketnidhi";
        private readonly IAmazonS3 _s3Client;

        public S3ImageUploadService()
        {
        }

        public async Task<string> UploadImageToS3(IFormFile file, string fileName)
        {
            var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast1);

            try
            {
                var bucketRequest = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };

                await s3Client.PutBucketAsync(bucketRequest);
            }
            catch(Exception ex)
            {

            }
            var fileTransferUtility = new TransferUtility(s3Client);

            var stream = new MemoryStream();

            await file.CopyToAsync(stream);

            var request = new TransferUtilityUploadRequest
            {
                BucketName = bucketName,
                InputStream = stream,
                Key = fileName,
                ContentType = "image/jpeg" 
            };

            await fileTransferUtility.UploadAsync(request);


            return string.Format("https://{0}.s3.amazonaws.com/{1}", bucketName, fileName);
        }

        public async Task DeleteImageFromS3(string fileName)
        {
            var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast1);

            var request = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = fileName
            };

            await s3Client.DeleteObjectAsync(request);
        }

        public async Task<string> FilUpload(IFormFile file)
        {
            string fileName = file.FileName;
            string objectKey = $"{fileName}";
            string books3url;
            try
            {
                using (Stream fileToUpload = file.OpenReadStream())
                {
                    var putObjectRequest = new PutObjectRequest();
                    putObjectRequest.BucketName = "bookstore-storage";
                    putObjectRequest.Key = objectKey;
                    putObjectRequest.InputStream = fileToUpload;
                    putObjectRequest.ContentType = file.ContentType;

                    var response = await _s3Client.PutObjectAsync(putObjectRequest);
                    books3url = GeneratePreSignedURL(objectKey);
                }
                return books3url;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
            return "";
        }

        private string GeneratePreSignedURL(string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "bookstore-storage",
                Key = objectKey,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            string url = _s3Client.GetPreSignedURL(request);
            return url;
        }

    }
}
