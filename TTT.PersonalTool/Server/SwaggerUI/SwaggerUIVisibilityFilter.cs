using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TTT.PersonalTool.Server.SwaggerUI;

public class SwaggerUIVisibilityFilter : IDocumentFilter
{
    private static List<string> _SwaggerHiddenGroups;

    public static List<string> SwaggerHiddenGroups
    {
        get
        {
            if (_SwaggerHiddenGroups == null)
            {
                _SwaggerHiddenGroups = new List<string>()
                {
                    //add new controller need to hidden
                };
            }

            return _SwaggerHiddenGroups;
        }
    }

    public SwaggerUIVisibilityFilter()
    {
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            var bHideMethod = SwaggerHiddenGroups.Contains(apiDescription.GroupName);
            if (bHideMethod)
            {
                var keypath = apiDescription.RelativePath;
                var removeRoutes = swaggerDoc.Paths.Where(x => x.Key.ToLower().Contains(keypath.ToLower())).ToList();
                removeRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
            }
        }
    }
}
