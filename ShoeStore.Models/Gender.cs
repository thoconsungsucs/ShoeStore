/*using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}
*/

namespace ShoeStore.Models
{
    public enum Gender
    {
        Men,
        Women,
        Kids,
        Unisex
    }
}
