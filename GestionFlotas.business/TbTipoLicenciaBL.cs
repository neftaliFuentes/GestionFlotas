using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbTipoLicenciaBL
	{
		private readonly FlotasContext _db;
		public TbTipoLicenciaBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbTipoLicenciaModel> Obtener(int _TbTipoLicenciaId)
		{
			var tipo = await (from p in _db.TbTipoLicencia
							  where p.TbTipoLicenciaId == _TbTipoLicenciaId
							  select (new TbTipoLicenciaModel
							  {
								  TbTipoLicenciaId = p.TbTipoLicenciaId,
								  Nombre = p.Nombre.Trim(),
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return tipo;
		}
		public IQueryable<TbTipoLicenciaModel> ListarAsQuerable()
		{
			var tipos = (from p in _db.TbTipoLicencia
						 select (new TbTipoLicenciaModel
						 {
							 TbTipoLicenciaId = p.TbTipoLicenciaId,
							 Nombre = p.Nombre.Trim(),
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).AsQueryable();

			return tipos;
		}
		public async Task Guardar(TbTipoLicenciaModel _TbTipoLicencia)
		{
			try
			{
				List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbTipoLicencia);
				if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbTipoLicencia oTipoLicencia = null;
				if (_TbTipoLicencia.TbTipoLicenciaId == 0)
				{
					oTipoLicencia = new TbTipoLicencia
					{
						TbTipoLicenciaId = _TbTipoLicencia.TbTipoLicenciaId,
						Nombre = _TbTipoLicencia.Nombre,
						Activo = _TbTipoLicencia.Activo,
					};
					_db.Add(oTipoLicencia);
				}
				else
				{
					oTipoLicencia = await _db.TbTipoLicencia.Where(x => x.TbTipoLicenciaId == _TbTipoLicencia.TbTipoLicenciaId).FirstAsync();
					if (oTipoLicencia == null) throw new Exception($"Tipo de licencia no existe para el ID: {_TbTipoLicencia.TbTipoLicenciaId}");

					oTipoLicencia.TbTipoLicenciaId = _TbTipoLicencia.TbTipoLicenciaId;
					oTipoLicencia.Nombre = _TbTipoLicencia.Nombre;
					oTipoLicencia.Activo = _TbTipoLicencia.Activo;

					_db.Update(oTipoLicencia);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbTipoLicenciaId)
		{
			try
			{
				return await _db.TbTipoLicencia.Where(x => x.TbTipoLicenciaId == _TbTipoLicenciaId).ExecuteDeleteAsync();

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
