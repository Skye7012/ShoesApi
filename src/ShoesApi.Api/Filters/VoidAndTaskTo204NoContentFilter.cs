using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShoesApi.Api.Filters;

/// <summary>
/// Фильтр, который возвращает код состояния 204, когда действие в контроллере ничего не возвращает
/// т.е. <see cref="void"/> or <see cref="Task"/>.
/// </summary>
public class VoidAndTaskTo204NoContentFilter : IResultFilter
{
	/// <inheritdoc/>
	public void OnResultExecuted(ResultExecutedContext context)
	{
		if (!context.ModelState.IsValid)
			return;

		if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
		{
			var returnType = actionDescriptor.MethodInfo.ReturnType;
			if (returnType == typeof(void) || returnType == typeof(Task))
			{
				context.HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
			}
		}
	}

	/// <inheritdoc/>
	public void OnResultExecuting(ResultExecutingContext context)
	{
	}
}
