using System;
using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null) return true; // [Required] se encarga si hace falta
            if (value is DateTime dt) return dt.Date <= DateTime.Today;
            return false;
        }
    }
}
