﻿using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            this._blogPostRepository = blogPostRepository;
        }


        //POST: {apibaseurl}/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            //Convert DTO to Domain model
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible
            };

            blogPost = await _blogPostRepository.CreateAsync(blogPost);
            //Convert Domain model to the DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                PublishedDate = blogPost.PublishedDate,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                IsVisible = blogPost.IsVisible,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
            };
            return Ok(response);
        }

        // GET : {apibaseurl}/api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            IEnumerable<BlogPost> blogPosts = await _blogPostRepository.GetAllAsync();

            //Convert the Domain model to DTO 
            var response = new List<BlogPostDto>();
            foreach (var blogPost in blogPosts) 
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    PublishedDate = blogPost.PublishedDate,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    IsVisible = blogPost.IsVisible,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                });
            }
            return Ok(response);
        }
    }
}
