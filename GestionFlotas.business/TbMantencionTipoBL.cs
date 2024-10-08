using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbMantencionTipoBL
	{
		private readonly FlotasContext _db;
		public TbMantencionTipoBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbMantencionTipoModel> Obtener(int _TbMantencionTipoId)
		{
			var tipo = await (from p in _db.TbMantencionTipo
							  where p.TbMantencionTipoId == _TbMantencionTipoId
							  select (new TbMantencionTipoModel
							  {
								  TbMantencionTipoId = p.TbMantencionTipoId,
								  Nombre = p.Nombre.Trim(),
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return tipo;
		}
		public IQueryable<TbMantencionTipoModel> ListarAsQuerable()
		{
			var tipos = (from p in _db.TbMantencionTipo
						 select (new TbMantencionTipoModel
						 {
							 TbMantencionTipoId = p.TbMantencionTipoId,
							 Nombre = p.Nombre.Trim(),
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).AsQueryable();

			return tipos;
		}
		public async Task Guardar(TbMantencionTipoModel _TbMantencionTipo)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbMantencionTipo);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbMantencionTipo oMantencionTipo = null;
				if (_TbMantencionTipo.TbMantencionTipoId == 0)
				{
					oMantencionTipo = new TbMantencionTipo
					{
						TbMantencionTipoId = _TbMantencionTipo.TbMantencionTipoId,
						Nombre = _TbMantencionTipo.Nombre,
						Activo = _TbMantencionTipo.Activo,
					};
					_db.Add(oMantencionTipo);
				}
				else
				{
					oMantencionTipo = await _db.TbMantencionTipo.Where(x => x.TbMantencionTipoId == _TbMantencionTipo.TbMantencionTipoId).FirstAsync();
					if (oMantencionTipo == null) throw new Exception($"Tipo de mantención no existe para el ID: {_TbMantencionTipo.TbMantencionTipoId}");

					oMantencionTipo.TbMantencionTipoId = _TbMantencionTipo.TbMantencionTipoId;
					oMantencionTipo.Nombre = _TbMantencionTipo.Nombre;
					oMantencionTipo.Activo = _TbMantencionTipo.Activo;

					_db.Update(oMantencionTipo);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbMantencionTipoId)
		{
			try
			{
				return await _db.TbMantencionTipo.Where(x => x.TbMantencionTipoId == _TbMantencionTipoId).ExecuteDeleteAsync();

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
