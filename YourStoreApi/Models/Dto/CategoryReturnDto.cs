namespace YourStoreApi.Models.Dto
{
    public class CategoryToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        // public int SubCategory_Id { get; set; }
        public ICollection<string> SubCategory { get; set; }=new List<string>();
    }
}
