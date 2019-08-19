namespace Tests
{
    public class Data
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }

        public override string ToString()
        {
            return "[id:" + id + ",email:" + email + ",first_name:" + first_name + ",last_name:" + last_name + ",avatar:" + avatar + "]";
        }
    }

    public class ApiData
    {
        public Data data { get; set; }

        public override string ToString()
        {
            return "Data: " + data;
        }
    }
}