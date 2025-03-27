using System.Globalization;

namespace finefin.api.Http.Middlewares
{
    public class CultureMiddleware
    {
        public readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            /*
             O CultureInfo é uma classe estática que pode ser usada diretamente, sem a necessidade de instanciá-la primeiro.
             */
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var cultureInfo = new CultureInfo("en");

            if (!string.IsNullOrWhiteSpace(requestedCulture) && supportedLanguages.Any(x => x.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
