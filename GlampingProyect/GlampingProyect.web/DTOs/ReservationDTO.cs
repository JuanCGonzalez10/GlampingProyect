namespace GlampingProyect.Web.Data.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public int GlampingUnitId { get; set; }
    }
}
