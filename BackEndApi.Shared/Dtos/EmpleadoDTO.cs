using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndApi.Shared.Dtos
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }
        
        [MaxLength(85, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public string NombreCompleto { get; set; } = null!;

        [MaxLength(85, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int IdDepartamento { get; set; }

        public string NombreDepartamento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        public decimal? Sueldo { get; set; }
        
        public string FechaContrato { get; set; }
    }
}
