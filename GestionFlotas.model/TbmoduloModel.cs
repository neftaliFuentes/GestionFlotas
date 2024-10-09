
namespace GestionFlotas.model
{
    public partial class TbmoduloModel
    {
        public short TbModuloId { get; set; }

        public string? Nombre { get; set; }

        public string? Icono { get; set; }

        public bool Activo { get; set; }

        public bool EsPadre { get; set; }

        public short PadreId { get; set; }

        public short Orden { get; set; }

        public string? ActivoString { get; set; }

        public List<TbModuloAccionModel> MisAcciones { get; set; }

        public TbmoduloModel()
        {
            Activo = false;
            MisAcciones = new List<TbModuloAccionModel>();
        }




    }
}
