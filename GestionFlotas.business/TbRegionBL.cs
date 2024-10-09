using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbRegionBL
	{
		private readonly FlotasContext _db;
		public TbRegionBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbRegionModel> Obtener(int _TbRegionId)
		{
			var region = await (from p in _db.TbRegion
								where p.TbRegionId == _TbRegionId
								select (new TbRegionModel
								{
									TbRegionId = p.TbRegionId,
									Nombre = p.Nombre.Trim(),
									Activo = p.Activo,
									ActivoString = p.Activo ? "SI" : "NO",
									NombrePais = p.TbPais.Nombre
								})).FirstOrDefaultAsync();


			return region;
		}
		public IQueryable<TbRegionModel> ListarAsQuerable()
		{
			var regions = (from p in _db.TbRegion
						   select (new TbRegionModel
						   {
							   TbRegionId = p.TbRegionId,
							   Nombre = p.Nombre.Trim(),
							   Activo = p.Activo,
							   ActivoString = p.Activo ? "SI" : "NO",
							   NombrePais = p.TbPais.Nombre
						   })).AsQueryable();

			return regions;
		}

		public async Task<List<TbRegionModel>> ListarByDDL()
		{
			var regions = await (from p in _db.TbRegion
								 where p.Activo
								 select (new TbRegionModel
								 {
									 TbRegionId = p.TbRegionId,
									 Nombre = p.Nombre.Trim(),
								 })).ToListAsync();

			return regions;
		}

		public async Task Guardar(TbRegionModel _TbRegion)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbRegion);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbRegion oRegion = null;
				if (_TbRegion.TbRegionId == 0)
				{
					oRegion = new TbRegion
					{
						TbPaisId = _TbRegion.TbPaisId,
						Nombre = _TbRegion.Nombre,
						Activo = _TbRegion.Activo,
					};
					_db.Add(oRegion);
				}
				else
				{
					oRegion = await _db.TbRegion.Where(x => x.TbRegionId == _TbRegion.TbRegionId).FirstAsync();
					if (oRegion == null) throw new Exception($"Región no existe para el ID: {_TbRegion.TbRegionId}");

					oRegion.TbPaisId = _TbRegion.TbPaisId;
					oRegion.Nombre = _TbRegion.Nombre;
					oRegion.Activo = _TbRegion.Activo;

					_db.Update(oRegion);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbRegionId)
		{
			try
			{
				return await _db.TbRegion.Where(x => x.TbRegionId == _TbRegionId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro region porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
	}
}
