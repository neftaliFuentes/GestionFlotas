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
		public async Task Guardar(TbVehiculoTipoModel _TbVehiculoTipo)
		{
			try
			{
				List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbVehiculoTipo);
				if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbVehiculoTipo oTipoVehiculo = null;
				if (_TbVehiculoTipo.TbVehiculoTipoId == 0)
				{
					oTipoVehiculo = new TbVehiculoTipo
					{
						TbVehiculoTipoId = _TbVehiculoTipo.TbVehiculoTipoId,
						Nombre = _TbVehiculoTipo.Nombre,
						Activo = _TbVehiculoTipo.Activo,
					};
					_db.Add(oTipoVehiculo);
				}
				else
				{
					oTipoVehiculo = await _db.TbVehiculoTipo.Where(x => x.TbVehiculoTipoId == _TbVehiculoTipo.TbVehiculoTipoId).FirstAsync();
					if (oTipoVehiculo == null) throw new Exception($"Tipo de vehículo no existe para el ID: {_TbVehiculoTipo.TbVehiculoTipoId}");

					oTipoVehiculo.TbVehiculoTipoId = _TbVehiculoTipo.TbVehiculoTipoId;
					oTipoVehiculo.Nombre = _TbVehiculoTipo.Nombre;
					oTipoVehiculo.Activo = _TbVehiculoTipo.Activo;

					_db.Update(oTipoVehiculo);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbVehiculoTipoId)
		{
			try
			{
				return await _db.TbVehiculoTipo.Where(x => x.TbVehiculoTipoId == _TbVehiculoTipoId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro tipo porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
	}
}
