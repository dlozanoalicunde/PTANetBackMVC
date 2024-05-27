namespace MBAOptionsManager.Model
{
    public class MBA
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int MBAOptionId { get; set; }
        public MBAOption MBAOption { get; set; }
    }
}