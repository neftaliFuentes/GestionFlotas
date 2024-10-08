using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbTallerBL
	{
		private readonly FlotasContext _db;
		public TbTallerBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbTallerModel> Obtener(int _TbTallerId)
		{
			var tipo = await (from p in _db.TbTaller
							  where p.TbTallerId == _TbTallerId
							  select (new TbTallerModel
							  {
								  TbTallerId = p.TbTallerId,
								  Nombre = p.Nombre.Trim(),
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return tipo;
		}
		public IQueryable<TbTallerModel> ListarAsQuerable()
		{
			var tipos = (from p in _db.TbTaller
						 select (new TbTallerModel
						 {
							 TbTallerId = p.TbTallerId,
							 Nombre = p.Nombre.Trim(),
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).AsQueryable();

			return tipos;
		}
		public async Task Guardar(TbTallerModel _TbTaller)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbTaller);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbTaller oTaller = null;
				if (_TbTaller.TbTallerId == 0)
				{
					oTaller = new TbTaller
					{
						TbTallerId = _TbTaller.TbTallerId,
						Nombre = _TbTaller.Nombre,
						Activo = _TbTaller.Activo,
					};
					_db.Add(oTaller);
				}
				else
				{
					oTaller = await _db.TbTaller.Where(x => x.TbTallerId == _TbTaller.TbTallerId).FirstAsync();
					if (oTaller == null) throw new Exception($"Taller no existe para el ID: {_TbTaller.TbTallerId}");

					oTaller.TbTallerId = _TbTaller.TbTallerId;
					oTaller.Nombre = _TbTaller.Nombre;
					oTaller.Activo = _TbTaller.Activo;

					_db.Update(oTaller);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbTallerId)
		{
			try
			{
				return await _db.TbTaller.Where(x => x.TbTallerId == _TbTallerId).ExecuteDeleteAsync();

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
