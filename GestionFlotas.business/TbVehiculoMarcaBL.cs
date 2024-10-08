using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbVehiculoMarcaBL
	{
		private readonly FlotasContext _db;
		public TbVehiculoMarcaBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbVehiculoMarcaModel> Obtener(int _TbVehiculoMarcaId)
		{
			var tipo = await (from p in _db.TbVehiculoMarca
							  where p.TbVehiculoMarcaId == _TbVehiculoMarcaId
							  select (new TbVehiculoMarcaModel
							  {
								  TbVehiculoMarcaId = p.TbVehiculoMarcaId,
								  Nombre = p.Nombre.Trim(),
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return tipo;
		}
		public IQueryable<TbVehiculoMarcaModel> ListarAsQuerable()
		{
			var tipos = (from p in _db.TbVehiculoMarca
						 select (new TbVehiculoMarcaModel
						 {
							 TbVehiculoMarcaId = p.TbVehiculoMarcaId,
							 Nombre = p.Nombre.Trim(),
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).AsQueryable();

			return tipos;
		}
		public async Task Guardar(TbVehiculoMarcaModel _TbVehiculoMarca)
		{
			try
			{
				List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbVehiculoMarca);
				if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbVehiculoMarca oMarcaVehiculo = null;
				if (_TbVehiculoMarca.TbVehiculoMarcaId == 0)
				{
					oMarcaVehiculo = new TbVehiculoMarca
					{
						TbVehiculoMarcaId = _TbVehiculoMarca.TbVehiculoMarcaId,
						Nombre = _TbVehiculoMarca.Nombre,
						Activo = _TbVehiculoMarca.Activo,
					};
					_db.Add(oMarcaVehiculo);
				}
				else
				{
					oMarcaVehiculo = await _db.TbVehiculoMarca.Where(x => x.TbVehiculoMarcaId == _TbVehiculoMarca.TbVehiculoMarcaId).FirstAsync();
					if (oMarcaVehiculo == null) throw new Exception($"Marca de vehículo no existe para el ID: {_TbVehiculoMarca.TbVehiculoMarcaId}");

					oMarcaVehiculo.TbVehiculoMarcaId = _TbVehiculoMarca.TbVehiculoMarcaId;
					oMarcaVehiculo.Nombre = _TbVehiculoMarca.Nombre;
					oMarcaVehiculo.Activo = _TbVehiculoMarca.Activo;

					_db.Update(oMarcaVehiculo);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbVehiculoMarcaId)
		{
			try
			{
				return await _db.TbVehiculoMarca.Where(x => x.TbVehiculoMarcaId == _TbVehiculoMarcaId).ExecuteDeleteAsync();

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
