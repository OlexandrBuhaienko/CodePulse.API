using CodePulse.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodePulse.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);
    }
}
