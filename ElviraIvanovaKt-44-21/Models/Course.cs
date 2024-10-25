/*namespace ElviraIvanovaKt_44_21.Models
{
    public class Course
    {
    }
}
*/
using ElviraIvanovaKt_44_21.Models;
using System.Text.Json.Serialization;

namespace _1_lab.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int GroupId { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }
        //public Group? Group { get; set; }
    }
}