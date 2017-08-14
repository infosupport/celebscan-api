namespace Celebscan.Service.Models
{
    public class WikipediaMatch
    {
        public WikipediaMatch(string pageId, string title, string extract, string pageImage)
        {
            PageId = pageId;
            Title = title;
            Extract = extract;
            PageImage = pageImage;
        }

        public string PageId { get; set; }
        public string Title { get; set; }
        public string Extract { get; set; }
        public string PageImage { get; set; }
    }
}