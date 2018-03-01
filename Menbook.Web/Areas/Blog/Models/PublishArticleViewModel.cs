namespace Menbook.Web.Areas.Blog.Models
{
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class PublishArticleViewModel
    {
        [Required]
        [MinLength(DataConstrants.ArticleTitleMinLength)]
        [MaxLength(DataConstrants.ArticleTitleMaxLength)]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
