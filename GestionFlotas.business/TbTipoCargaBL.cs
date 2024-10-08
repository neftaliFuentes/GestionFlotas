using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbTipoCargaBL
	{
		private readonly FlotasContext _db;
		public TbTipoCargaBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbTipoCargaModel> Obtener(int _TbTipoCargaId)
		{
			var tipo = await (from p in _db.TbTipoCarga
							  where p.TbTipoCargaId == _TbTipoCargaId
							  select (new TbTipoCargaModel
							  {
								  TbTipoCargaId = p.TbTipoCargaId,
								  Nombre = p.Nombre.Trim(),
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return tipo;
		}
		public IQueryable<TbTipoCargaModel> ListarAsQuerable()
		{
			var tipos = (from p in _db.TbTipoCarga
						 select (new TbTipoCargaModel
						 {
							 TbTipoCargaId = p.TbTipoCargaId,
							 Nombre = p.Nombre.Trim(),
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).AsQueryable();

			return tipos;
		}
		public async Task Guardar(TbTipoCargaModel _TbTipoCarga)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbTipoCarga);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbTipoCarga oTipoCarga = null;
				if (_TbTipoCarga.TbTipoCargaId == 0)
				{
					oTipoCarga = new TbTipoCarga
					{
						TbTipoCargaId = _TbTipoCarga.TbTipoCargaId,
						Nombre = _TbTipoCarga.Nombre,
						Activo = _TbTipoCarga.Activo,
					};
					_db.Add(oTipoCarga);
				}
				else
				{
					oTipoCarga = await _db.TbTipoCarga.Where(x => x.TbTipoCargaId == _TbTipoCarga.TbTipoCargaId).FirstAsync();
					if (oTipoCarga == null) throw new Exception($"Tipo de carga no existe para el ID: {_TbTipoCarga.TbTipoCargaId}");

					oTipoCarga.TbTipoCargaId = _TbTipoCarga.TbTipoCargaId;
					oTipoCarga.Nombre = _TbTipoCarga.Nombre;
					oTipoCarga.Activo = _TbTipoCarga.Activo;

					_db.Update(oTipoCarga);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbTipoCargaId)
		{
			try
			{
				return await _db.TbTipoCarga.Where(x => x.TbTipoCargaId == _TbTipoCargaId).ExecuteDeleteAsync();

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
