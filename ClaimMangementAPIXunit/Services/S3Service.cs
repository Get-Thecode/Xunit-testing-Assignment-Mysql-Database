//using Amazon.S3.Transfer;
//using Amazon.S3;

//namespace ClaimsManagementApi.Services
//{
//    public class S3Service
//    {
//        private readonly IAmazonS3 _s3Client;
//        private readonly string _bucketName;

//        public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
//        {
//            _s3Client = s3Client;
//            _bucketName = configuration["AWS:BucketName"];
//        }

//        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
//        {
//            var fileTransferUtility = new TransferUtility(_s3Client);
//            var uploadRequest = new TransferUtilityUploadRequest
//            {
//                InputStream = fileStream,
//                Key = $"uploads/{Guid.NewGuid()}_{fileName}",  // Path within the bucket
//                BucketName = _bucketName,
//                ContentType = "application/octet-stream"
//            };

//            await fileTransferUtility.UploadAsync(uploadRequest);
//            return $"https://{_bucketName}.s3.amazonaws.com/{uploadRequest.Key}";
//        }
//    }
//}
