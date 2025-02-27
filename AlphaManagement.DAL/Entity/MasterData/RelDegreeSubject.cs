namespace AlphaManagement.DAL.Entity.MasterData
{
    public class RelDegreeSubject:Base
    {
        public int degreeId { get; set; }
        public Degree degree { get; set; }

        public int subjectId { get; set; }
        public Subject subject { get; set; }
    }
}
