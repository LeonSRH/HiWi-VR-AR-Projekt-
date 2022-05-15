namespace SmartHospital.Controller.ExplorerMode.Rooms.Details
{
    public class SearchDetails
    {
        public Floor _floornumber { get; set; }
        public string _roomnumber { get; set; }
        public Buildingsection _buildingsection { get; set; }
        public Specialistdepartment _specialistdepartment { get; set; }
        public int _workingstations { get; set; }
        public string _cost_centre { get; set; }
        public Roomdesignation _roomdesignation { get; set; }


        public enum Floor
        {
            FLOOR_98, FLOOR_99, FLOOR_00, FLOOR_01, FLOOR_02, FLOOR_03, FLOOR_04, FLOOR_EMPTY
        }

        public enum Buildingsection
        {
            A, B, C, D, E, F, EMPTY
        }

        public enum Specialistdepartment
        {
            GENERAL, HEICUMED, ANAESTHESIA, VISCERALSURGERY, VASCULARSURGERY, ACCIDENTSURGERY, UROLOGY, HEARTSURGERY, NONE
        }

        public enum Roomdesignation
        {
            SERVICEROOM, EMERGENCYSERVICEROOM, SITTINGROOM, COPYROOM,
            WAREHOUSE, MEETINGROOM, OP, PATIENTROOM, UANDB, CHANGINGROOM, WAITINGROOM,
            TECHNOLOGY
        }
    }
}
