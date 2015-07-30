namespace Data.Dto
{
    public class Food
    {
        public string Name { get; set; }
        public MainCategory MainCategory { get; set; }
        public SubCategory SubCategory { get; set; }
        public string Id { get; set; }
        public string Calories { get; set; }
        public string EdiblePartPercent { get; set; }
        public string KiloJoules { get; set; }
        public string WaterGrams { get; set; }
    }
}