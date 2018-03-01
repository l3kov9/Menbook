namespace Menbook.Web.Areas.Blog.Models
{
    using Services.Models.Blog;
    using System.Collections.Generic;

    public class ArticleListingViewModel
    {
        public IEnumerable<ArticleListingServiceModel> Articles { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage => this.CurrentPage >= this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public string CurrentUserName { get; set; }
    }
}
