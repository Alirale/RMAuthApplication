namespace Core.DTOs
{
    public class PeapleInfoDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string BirthDate { get; set; }
        public int NationalCode { get; set; }
        public bool IsMarried { get; set; }
        public int UserAccessID { get; set; }
    }
}
