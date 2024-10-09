using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbMunicipalidadBL
	{
		private readonly FlotasContext _db;
		public TbMunicipalidadBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbMunicipalidadModel> Obtener(int _TbMunicipalidadId)
		{
			var municipalidad = await (from p in _db.TbMunicipalidad
								where p.TbMunicipalidadId == _TbMunicipalidadId
								select (new TbMunicipalidadModel
								{
									TbMunicipalidadId = p.TbMunicipalidadId,
									TbComunaId = p.TbComunaId,
									Nombre = p.Nombre.Trim(),
									Activo = p.Activo,
									ActivoString = p.Activo ? "SI" : "NO",
									NombreComuna = p.TbComuna.Nombre,
									NombreRegion = p.TbComuna.TbRegion.Nombre
								})).FirstOrDefaultAsync();


			return municipalidad;
		}
		public IQueryable<TbMunicipalidadModel> ListarAsQuerable()
		{
			var municipalidads = (from p in _db.TbMunicipalidad
						   select (new TbMunicipalidadModel
						   {
							   TbMunicipalidadId = p.TbMunicipalidadId,
							   TbComunaId = p.TbComunaId,
							   Nombre = p.Nombre.Trim(),
							   Activo = p.Activo,
							   ActivoString = p.Activo ? "SI" : "NO",
							   NombreComuna = p.TbComuna.Nombre,
							   NombreRegion = p.TbComuna.TbRegion.Nombre
						   })).AsQueryable();

			return municipalidads;
		}
		public async Task<List<TbMunicipalidadModel>> ListarByDDL()
		{
			var municipalidads = await (from p in _db.TbMunicipalidad
								 where p.Activo
								 select (new TbMunicipalidadModel
								 {
									 TbMunicipalidadId = p.TbMunicipalidadId,
									 Nombre = p.Nombre.Trim(),
								 })).ToListAsync();

			return municipalidads;
		}
		public async Task Guardar(TbMunicipalidadModel _TbMunicipalidad)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbMunicipalidad);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbMunicipalidad oMunicipalidad = null;
				if (_TbMunicipalidad.TbMunicipalidadId == 0)
				{
					oMunicipalidad = new TbMunicipalidad
					{
						TbComunaId = _TbMunicipalidad.TbComunaId,
						Nombre = _TbMunicipalidad.Nombre,
						Activo = _TbMunicipalidad.Activo,
					};
					_db.Add(oMunicipalidad);
				}
				else
				{
					oMunicipalidad = await _db.TbMunicipalidad.Where(x => x.TbMunicipalidadId == _TbMunicipalidad.TbMunicipalidadId).FirstAsync();
					if (oMunicipalidad == null) throw new Exception($"Municipalidad no existe para el ID: {_TbMunicipalidad.TbMunicipalidadId}");

					oMunicipalidad.TbComunaId = _TbMunicipalidad.TbComunaId;
					oMunicipalidad.Nombre = _TbMunicipalidad.Nombre;
					oMunicipalidad.Activo = _TbMunicipalidad.Activo;

					_db.Update(oMunicipalidad);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbMunicipalidadId)
		{
			try
			{
				return await _db.TbMunicipalidad.Where(x => x.TbMunicipalidadId == _TbMunicipalidadId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro municipalidad porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
	}
}
