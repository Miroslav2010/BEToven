
using System.ComponentModel.DataAnnotations;

namespace BETOven.Enums
{
    public enum SportsEnum
    {
        [Display(Name = "Football")]
        Football = 0,
        [Display(Name = "Basketball")]
        Basketball=1,
        [Display(Name = "Handball")]
        Handball=2
    }
}