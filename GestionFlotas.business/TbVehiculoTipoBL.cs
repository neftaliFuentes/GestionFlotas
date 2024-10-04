using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
    public class TbVehiculoTipoBL
	{
		private readonly FlotasContext _db;
		public TbVehiculoTipoBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbVehiculoTipoModel> Obtener(int _TbVehiculoTipoId)
		{
			var tipo = await (from p in _db.TbVehiculoTipo
								where p.TbVehiculoTipoId == _TbVehiculoTipoId
								select (new TbVehiculoTipoModel
								{
									TbVehiculoTipoId = p.TbVehiculoTipoId,
									Nombre = p.Nombre.Trim(),
									Activo = p.Activo,
									ActivoString = p.Activo ? "SI":"NO",
								})).FirstOrDefaultAsync();


			return tipo;
		}
		public IQueryable<TbVehiculoTipoModel> ListarAsQuerable()
		{
			var tipos = (from p in _db.TbVehiculoTipo
						   select (new TbVehiculoTipoModel
						   {
							   TbVehiculoTipoId = p.TbVehiculoTipoId,
							   Nombre = p.Nombre.Trim(),
							   Activo = p.Activo,
							   ActivoString = p.Activo ? "SI" : "NO",
						   })).AsQueryable();

			return tipos;
		}
	}
}
