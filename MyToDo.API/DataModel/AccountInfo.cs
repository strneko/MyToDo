using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyToDo.API.DataModel
{
    [Table("AccountInfo")]
    public class AccountInfo
    {
        [Key]
        public int AccountId { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPassword { get; set; }

    }
}
