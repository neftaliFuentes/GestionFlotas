using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlotas.model
{
    public class TbModuloAccionModel
    {
        public short TbModuloAccionId { get; set; }

        public short TbModuloId { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Icono { get; set; }
        public string? Url { get; set; }

        public bool EsVisibleMenu { get; set; }

        public bool Activo { get; set; }
    }
}
