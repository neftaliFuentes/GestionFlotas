using GestionFlotas.model;
using System.ComponentModel.DataAnnotations;

namespace GestionFlotas.business
{
	public static class ValidadorModelBL
	{
		public static List<ErrorValidacionModel> valida(object modelo)
		{
			var context = new ValidationContext(modelo, serviceProvider: null, items: null);
			var validationResults = new List<ValidationResult>();
			Validator.TryValidateObject(modelo, context, validationResults, true);
			List<ErrorValidacionModel> errores = new List<ErrorValidacionModel>();
			if (validationResults.Count > 0)
				foreach (var validationResult in validationResults)
					errores.Add(new ErrorValidacionModel { Mensaje = validationResult.ErrorMessage.Trim() });
			return errores;
		}
	}
}
