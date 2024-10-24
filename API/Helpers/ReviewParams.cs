namespace API.Helpers
{
    public class ReviewParams : PaginationParams
    {
        public string OrderBy { get; set; } = "id_desc";
        public int Rating { get; set; } // rating & 6 have image / video 
    }
}
