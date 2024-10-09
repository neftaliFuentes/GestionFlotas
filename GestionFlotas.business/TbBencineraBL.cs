using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbBencineraBL
	{
		private readonly FlotasContext _db;
		public TbBencineraBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbBencineraModel> Obtener(int _TbBencineraId)
		{
			var comuna = await (from p in _db.TbBencinera
								where p.TbBencineraId == _TbBencineraId
								select (new TbBencineraModel
								{
									TbBencineraId = p.TbBencineraId,
									TbComunaId = p.TbComunaId,
									Nombre = p.Nombre.Trim(),
									Direccion = p.Direccion,
									Activo = p.Activo,
									ActivoString = p.Activo ? "SI" : "NO",
									NombreComuna = p.TbComuna.Nombre,
									NombreRegion = p.TbComuna.TbRegion.Nombre
								})).FirstOrDefaultAsync();


			return comuna;
		}
		public IQueryable<TbBencineraModel> ListarAsQuerable()
		{
			var bencineras = (from p in _db.TbBencinera
						   select (new TbBencineraModel
						   {
							   TbBencineraId = p.TbBencineraId,
							   TbComunaId = p.TbComunaId,
							   Nombre = p.Nombre.Trim(),
							   Direccion = p.Direccion,
							   Activo = p.Activo,
							   ActivoString = p.Activo ? "SI" : "NO",
							   NombreComuna = p.TbComuna.Nombre,
							   NombreRegion = p.TbComuna.TbRegion.Nombre
						   })).AsQueryable();

			return bencineras;
		}
		public async Task<List<TbBencineraModel>> ListarByDDL()
		{
			var bencineras = await (from p in _db.TbBencinera
								 where p.Activo
								 select (new TbBencineraModel
								 {
									 TbBencineraId = p.TbBencineraId,
									 Nombre = p.Nombre.Trim(),
								 })).ToListAsync();

			return bencineras;
		}
		public async Task Guardar(TbBencineraModel _TbBencinera)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbBencinera);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbBencinera oComuna = null;
				if (_TbBencinera.TbBencineraId == 0)
				{
					oComuna = new TbBencinera
					{
						TbComunaId = _TbBencinera.TbComunaId,
						Direccion = _TbBencinera.Direccion,
						Nombre = _TbBencinera.Nombre,
						Activo = _TbBencinera.Activo,
					};
					_db.Add(oComuna);
				}
				else
				{
					oComuna = await _db.TbBencinera.Where(x => x.TbBencineraId == _TbBencinera.TbBencineraId).FirstAsync();
					if (oComuna == null) throw new Exception($"Comuna no existe para el ID: {_TbBencinera.TbBencineraId}");

					oComuna.TbComunaId = _TbBencinera.TbComunaId;
					oComuna.Direccion = _TbBencinera.Direccion;
					oComuna.Nombre = _TbBencinera.Nombre;
					oComuna.Activo = _TbBencinera.Activo;

					_db.Update(oComuna);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbBencineraId)
		{
			try
			{
				return await _db.TbBencinera.Where(x => x.TbBencineraId == _TbBencineraId).ExecuteDeleteAsync();

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
