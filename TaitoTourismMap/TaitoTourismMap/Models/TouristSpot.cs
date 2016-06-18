namespace TaitoTourismMap
{
    public class TouristSpot
    {
        public class Rootobject
        {
            public Tourspot[] tourspots { get; set; }
        }

        public class Tourspot
        {
            public Name name { get; set; }
            public Genre[] genres { get; set; }
            public View[] views { get; set; }
            public Desc[] descs { get; set; }
            public Place place { get; set; }
            public Mng mng { get; set; }
        }

        public class Name
        {
            public Name1 name1 { get; set; }
        }

        public class Name1
        {
            public string written { get; set; }
            public string spoken { get; set; }
        }

        public class Place
        {
            public string postal_code { get; set; }
            public Pref pref { get; set; }
            public City city { get; set; }
            public Street street { get; set; }
        }

        public class Pref
        {
            public string written { get; set; }
        }

        public class City
        {
            public string written { get; set; }
        }

        public class Street
        {
            public string written { get; set; }
        }

        public class Mng
        {
            public string lgcode { get; set; }
            public string refbase { get; set; }
            public int refsub { get; set; }
            public Status status { get; set; }
            public string data_source { get; set; }
        }

        public class Status
        {
            public string update { get; set; }
        }

        public class Genre
        {
            public string L { get; set; }
            public string M { get; set; }
            public string S { get; set; }
        }

        public class View
        {
            public Name2 name { get; set; }
            public string copyright { get; set; }
            public string fid { get; set; }
        }

        public class Name2
        {
            public string written { get; set; }
            public string spoken { get; set; }
        }

        public class Desc
        {
            public string body { get; set; }
        }
    }
}