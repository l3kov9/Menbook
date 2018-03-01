namespace Menbook.Data.Models.Blog
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstrants.ArticleTitleMinLength)]
        [MaxLength(DataConstrants.ArticleTitleMaxLength)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
