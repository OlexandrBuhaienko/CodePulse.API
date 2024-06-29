﻿using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace CodePulse.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }
        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            //1- Upload the Image to API/Images
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            //2- Update the database

            //https://codeppulse.com/images/somefilename.jpg
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
            blogImage.Url = urlPath;
            await _dbContext.BlogImages.AddAsync(blogImage);
            await _dbContext.SaveChangesAsync ();
            return blogImage;
        }
    }
}
