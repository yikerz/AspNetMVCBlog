﻿/* 273. Create new view model for displaying comments */
namespace Blog.Web.Models.View
{
    public class BlogComment
    {
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string Username { get; set; }
    }
}
