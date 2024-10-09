using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbComunaBL
	{
		private readonly FlotasContext _db;
		public TbComunaBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbComunaModel> Obtener(int _TbComunaId)
		{
			var comuna = await (from p in _db.TbComuna
								where p.TbComunaId == _TbComunaId
								select (new TbComunaModel
								{
									TbComunaId = p.TbComunaId,
									TbRegionId = p.TbRegionId,
									Nombre = p.Nombre.Trim(),
									Activo = p.Activo,
									ActivoString = p.Activo ? "SI" : "NO",
									NombreRegion = p.TbRegion.Nombre
								})).FirstOrDefaultAsync();


			return comuna;
		}
		public IQueryable<TbComunaModel> ListarAsQuerable()
		{
			var comunas = (from p in _db.TbComuna
						   select (new TbComunaModel
						   {
							   TbComunaId = p.TbComunaId,
							   TbRegionId = p.TbRegionId,
							   Nombre = p.Nombre.Trim(),
							   Activo = p.Activo,
							   ActivoString = p.Activo ? "SI" : "NO",
							   NombreRegion = p.TbRegion.Nombre
						   })).AsQueryable();

			return comunas;
		}
		public async Task<List<TbComunaModel>> ListarByDDL()
		{
			var comunas = await (from p in _db.TbComuna
								 where p.Activo
								 select (new TbComunaModel
								 {
									 TbComunaId = p.TbComunaId,
									 Nombre = p.Nombre.Trim(),
								 })).ToListAsync();

			return comunas;
		}
		public async Task Guardar(TbComunaModel _TbComuna)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbComuna);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbComuna oComuna = null;
				if (_TbComuna.TbComunaId == 0)
				{
					oComuna = new TbComuna
					{
						TbRegionId = _TbComuna.TbRegionId,
						Nombre = _TbComuna.Nombre,
						Activo = _TbComuna.Activo,
					};
					_db.Add(oComuna);
				}
				else
				{
					oComuna = await _db.TbComuna.Where(x => x.TbComunaId == _TbComuna.TbComunaId).FirstAsync();
					if (oComuna == null) throw new Exception($"Comuna no existe para el ID: {_TbComuna.TbComunaId}");

					oComuna.TbRegionId = _TbComuna.TbRegionId;
					oComuna.Nombre = _TbComuna.Nombre;
					oComuna.Activo = _TbComuna.Activo;

					_db.Update(oComuna);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbComunaId)
		{
			try
			{
				return await _db.TbComuna.Where(x => x.TbComunaId == _TbComunaId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro comuna porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
	}
}
